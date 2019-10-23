using LogParser.Model;
using LogParser.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace LogParser.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TableData> tableDataList;
        private ObservableCollection<EventData> eventDataList;

        private string fileName;
        private string datePeriod;
        private string notesFromCarriageWithSoftStartup;
        private string numericData;
        private string eventsData;
        private bool isFileOpened;

        List<EventData> listEvent = new List<EventData>();
        List<TableData> listTable= new List<TableData>();

        public string[] Result;

        public ICommand OpenCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand OpenTableViewCommand { get; set; }

        public MainWindowViewModel()
        {
            tableDataList = new ObservableCollection<TableData>();
            eventDataList = new ObservableCollection<EventData>();
            IsFileOpened = false;
            BindingCommandsToClickMethods();
        }

        #region Properties
        public ObservableCollection<TableData> TableDataList
        {
            get
            {
                return tableDataList;
            }
            set
            {
                tableDataList = value;
                NotifyPropertyChanged("TableDataList");
            }
        }

        public ObservableCollection<EventData> EventDataList
        {
            get
            {
                return eventDataList;
            }
            set
            {
                eventDataList = value;
                NotifyPropertyChanged("EventDataList");
            }
        }

        public string FileName
        {
            get { return fileName; }
            set
            {
                if (fileName != value)
                {
                    fileName = value;
                    NotifyPropertyChanged("FileName");
                }
            }
        }
        public string DatePeriod
        {
            get { return datePeriod; }
            set
            {
                if (datePeriod != value)
                {
                    datePeriod = value;
                    NotifyPropertyChanged("DatePeriod");
                }
            }
        }
        public string NotesFromCarriageWithSoftStartup
        {
            get { return notesFromCarriageWithSoftStartup; }
            set
            {
                if (notesFromCarriageWithSoftStartup != value)
                {
                    notesFromCarriageWithSoftStartup = value;
                    NotifyPropertyChanged("NotesFromCarriageWithSoftStartup");
                }
            }
        }
        public string NumericData
        {
            get { return numericData; }
            set
            {
                if (numericData != value)
                {
                    numericData = value;
                    NotifyPropertyChanged("NumericData");
                }
            }
        }
        public string EventsData
        {
            get { return eventsData; }
            set
            {
                if (eventsData != value)
                {
                    eventsData = value;
                    NotifyPropertyChanged("EventsData");
                }
            }
        }
        public bool IsFileOpened
        {
            get { return isFileOpened; }
            set
            {
                if (isFileOpened != value)
                {
                    isFileOpened = value;
                    NotifyPropertyChanged("IsFileOpened");
                }
            }
        }
        #endregion

        private void BindingCommandsToClickMethods()
        {
            OpenCommand = new Command(arg => OpenClick());
            CloseCommand = new Command(arg => CloseClick());

            OpenTableViewCommand = new Command(arg => OpenTableViewClick());
        }

        private void OpenClick()
        {
            OpenFile();

            if (IsFileOpened)
            {
                SetInfoAboutLogFile();
                IsFileOpened = true;
            }
        }

        #region Create class SetInfoAboutLogFile
        private void SetInfoAboutLogFile()
        {
            SetFileName(FileName);

            //SetDatePeriod(StartDate, FinishDate);

            //SetNotesFromCarriageWithSoftStartup(NumberOfRecordsFromCarriageWithSoftStartup);

            //SetNumberOfNumericData(NumberOfRecordsWithNumericData);
            //SetNumberOfEventsData(NumberOfRecordsWithEventsData);
        }

        private void SetFileName(string fileName)
        {
            FileName = "Имя файла:  " + fileName;
        }

        private void SetDatePeriod(DateTime startDate, DateTime finishDate)
        {
            DatePeriod = "Период:  с " + startDate + " по " + finishDate;
        }

        private void SetNotesFromCarriageWithSoftStartup(string notesCount)
        {
            NotesFromCarriageWithSoftStartup = "Записей от вагона с устройством плавного " +
                "пуска (УПП):  " + notesCount;
        }

        private void SetNumberOfNumericData(string numberOfNumericData)
        {
            NumericData = "Из них с числовыми данными:  " + numberOfNumericData;
        }

        private void SetNumberOfEventsData(string numberOfEventsData)
        {
            EventsData = "Из них с событиями:  " + numberOfEventsData;
        }
        #endregion

        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Бинарные файлы (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.SafeFileName;
                ParseLogFileByBytesNew(openFileDialog.FileName);

                IsFileOpened = true;
            }
            else
            {
                IsFileOpened = false;
            }
        }

        public void ParseLogFileByBytesNew(string fileName)
        {
            List<byte[]> listOfRequests = new List<byte[]>();

            using (BinaryReader b = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                byte[][] DataMatrix = new byte[][] { };

                for (int i = 0; i < b.BaseStream.Length; i += 512)
                {
                    b.BaseStream.Seek(i, SeekOrigin.Begin);
                    if (b.PeekChar() == 'L')
                    {
                        listOfRequests.Add(b.ReadBytes(512));
                    }
                }

                foreach (byte[] request in listOfRequests)
                {
                    ParseRequest(request);
                }
            }

            EventDataList = new ObservableCollection<EventData>(listEvent);
            TableDataList = new ObservableCollection<TableData>(listTable);
        }

        private void ParseRequest(byte[] request)
        {
            //byte[] requestTitle = { request[0], request[1], request[2], request[3] };

            byte[] dateOfFirstRequest = { request[4], request[5], request[6], request[7] };
            byte[] dateOfFirstMessage = { request[8], request[9], request[10], request[11] };
            byte[] dateOfLastMessage = { request[12], request[13], request[14], request[15] };

            byte eventIdentifier = 65;

            int date1 = ConvertBytesArrayToIntValue(dateOfFirstRequest);
            int date2 = ConvertBytesArrayToIntValue(dateOfFirstMessage);
            int date3 = ConvertBytesArrayToIntValue(dateOfLastMessage);

            SetDateOfMessageSending(UnixTimeStampToDateTime(date2), UnixTimeStampToDateTime(date3));

            if (request[19] == 88 && request[20] == 49)
            {
                TableData tbl = new TableData();
                ParseTableDataFromLogFile(tbl, request);
                listTable.Add(tbl);
            }
            else if (request[17] == eventIdentifier)
            {
                string infoAboutEvent = Encoding.Default.GetString(request, 19, 64);
                
                EventData emp = new EventData();
                ParseEventDataFromLogFile(emp, infoAboutEvent);
                listEvent.Add(emp);
            }
            else
            {
                //Console.WriteLine("Unknown request");
            }
        }

        private List<DateTime> SetDateOfMessageSending(DateTime dateOfFirstMessage, DateTime dateOfLastMessage)
        {
            List<DateTime> MessageSendingDateList = new List<DateTime>();

            while (dateOfFirstMessage <= dateOfLastMessage)
            {
                MessageSendingDateList.Add(dateOfFirstMessage);
                dateOfFirstMessage = dateOfFirstMessage.AddSeconds(1);
            }

            return MessageSendingDateList;
        }

        #region ConvertClass
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }

        public int ConvertBytesArrayToIntValue(byte[] byteArray)
        {
            return BitConverter.ToInt32(byteArray, 0);
        }

        public int ConvertBytesArrayToInt16Value(byte[] byteArray)
        {
            return BitConverter.ToInt16(byteArray, 0);
        }
        #endregion

        public void ParseTableDataFromLogFile(TableData e, byte[] request)
        {
            byte[] VoltageA = { request[21], request[22] };
            byte[] VoltageB = { request[23], request[24] };
            byte[] VoltageC = { request[25], request[26] };

            byte[] AmperageA = { request[27], request[28] };
            byte[] AmperageB = { request[29], request[30] };
            byte[] AmperageC = { request[31], request[32] };

            byte[] Loil = { request[33], request[34] };
            byte[] Toil = { request[35], request[36] };

            byte Poil = request[37];
            byte Temperature = request[38];

            int currentVoltageA = ConvertBytesArrayToInt16Value(VoltageA);
            int currentVoltageB = ConvertBytesArrayToInt16Value(VoltageB);
            int currentVoltageC = ConvertBytesArrayToInt16Value(VoltageC);

            int currentAmperageA = ConvertBytesArrayToInt16Value(AmperageA);
            int currentAmperageB = ConvertBytesArrayToInt16Value(AmperageB);
            int currentAmperageC = ConvertBytesArrayToInt16Value(AmperageC);

            int currentLoil = ConvertBytesArrayToInt16Value(Loil);
            int currentToil = ConvertBytesArrayToInt16Value(Toil);

            int currentPoil = Convert.ToInt32(Poil);
            int currentTemperature = Convert.ToInt32(Temperature);

            e.VoltageA = currentVoltageA;
            e.VoltageB = currentVoltageB;
            e.VoltageC = currentVoltageC;

            e.AmperageA = currentAmperageA;
            e.AmperageB = currentAmperageB;
            e.AmperageC = currentAmperageC;

            e.Loil = currentLoil;
            e.Toil = currentToil;

            e.Poil = currentPoil;
            e.ThyristorTemperature = currentTemperature;
        }

        public void ParseEventDataFromLogFile(EventData e, string eventData)
        {
            Regex dateInEventData = new Regex(@"\d\d:\d\d:\d\d (\d\d)/(\d\d)/(\d\d)");
            Regex messageInEventData = new Regex(@".{40}");
            Regex statusInEventData = new Regex(@"A|P");

            Match eventDateMatch = dateInEventData.Match(eventData);
            Match statusDataMatch = statusInEventData.Match(eventData);
            Match messageDataMatch = messageInEventData.Match(eventData);

            if (eventDateMatch.Success)
            {
                e.Message = Convert.ToString(messageDataMatch);

                string dateTime = Convert.ToString(eventDateMatch);

                e.Date = DateTime.Parse(dateTime);
                e.Status = Convert.ToString(statusDataMatch);
            }
            else
            {
                //Console.WriteLine("Don't find any values");
            }
        }

        private void OpenTableViewClick()
        {
            var vm = new TableDataViewModel(EventDataList, TableDataList);
            var connectSettingView = new TableDataView
            {
                DataContext = vm
            };
            vm.OnRequestClose += (s, e) => connectSettingView.Close();
            connectSettingView.ShowDialog();
        }

        private void CloseClick()
        {
            IsFileOpened = false;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
    
}
