﻿using LogParser.Model;
using LogParser.View;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace LogParser.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public static int eventCounter = 0;
        public static int tableDataCounter = 0;

        List<EventData> listEvent = new List<EventData>();
        List<TableData> listTable = new List<TableData>();
        List<EventJoinedWithTableData> listJoin = new List<EventJoinedWithTableData>();

        public static List<DateTime> TableDataDate { get; set; }
        public static DateTime DateOfMessageInRequest { get; set; }

        public ICommand OpenCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand OpenTableViewCommand { get; set; }

        public MainWindowViewModel()
        {
            tableDataList = new ObservableCollection<TableData>();
            eventDataList = new ObservableCollection<EventData>();
            eventJoinedWithTableDataList = new ObservableCollection<EventJoinedWithTableData>();

            IsFileOpened = false;

            BindingCommandsToClickMethods();
        }

        #region Properties
        private ObservableCollection<TableData> tableDataList;
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

        private ObservableCollection<EventData> eventDataList;
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

        private ObservableCollection<EventJoinedWithTableData> eventJoinedWithTableDataList;
        public ObservableCollection<EventJoinedWithTableData> EventJoinedWithTableDataList
        {
            get
            {
                return eventJoinedWithTableDataList;
            }
            set
            {
                eventJoinedWithTableDataList = value;
                NotifyPropertyChanged("EventJoinedWithTableDataList");
            }
        }

        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set
            {
                if (fileName != value)
                {
                    fileName ="Имя файла:  " + value;
                    NotifyPropertyChanged("FileName");
                }
            }
        }

        private string datePeriod;
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

        private string notesFromCarriageWithSoftStartup;
        public string NotesFromCarriageWithSoftStartup
        {
            get { return notesFromCarriageWithSoftStartup; }
            set
            {
                if (notesFromCarriageWithSoftStartup != value)
                {
                    notesFromCarriageWithSoftStartup = "Записей от вагона с устройством плавного " +
                        "пуска (УПП):  " + value;
                    NotifyPropertyChanged("NotesFromCarriageWithSoftStartup");
                }
            }
        }

        private string numericDataCount;
        public string NumericDataCount
        {
            get { return numericDataCount; }
            set
            {
                if (numericDataCount != value)
                {
                    numericDataCount = "Из них с числовыми данными:  " + value;
                    NotifyPropertyChanged("NumericDataCount");
                }
            }
        }

        private string eventsDataCount;
        public string EventsDataCount
        {
            get { return eventsDataCount; }
            set
            {
                if (eventsDataCount != value)
                {
                    eventsDataCount = "Из них с событиями:  " + value;
                    NotifyPropertyChanged("EventsDataCount");
                }
            }
        }

        private bool isFileOpened;
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
                JoinEventAndTable();
                IsFileOpened = true;
            }
        }

        private void JoinEventAndTable()
        {         
            ObservableCollection<TableData> test = new ObservableCollection<TableData>();

            foreach (EventData eventDataItem in eventDataList)
            {
                EventJoinedWithTableData eventJoined = new EventJoinedWithTableData
                {
                    ID = eventDataItem.ID,
                    Date = eventDataItem.Date,
                    Message = eventDataItem.Message,
                    Status = eventDataItem.Status
                };

                int i = 0;

                foreach (TableData tableDataItem in tableDataList)
                {
                    if(tableDataItem.Date >= eventJoined.Date.AddSeconds(-5))
                    {  
                        test.Add(tableDataItem);

                        i++;

                        if(i == 10) break;
                    }
                }

                eventJoined.TableDatas = test;
                listJoin.Add(eventJoined);

                test = new ObservableCollection<TableData>();
            }

            EventJoinedWithTableDataList = new ObservableCollection<EventJoinedWithTableData>(listJoin);
            //eventJoinedWithTableData.TableDatas = TableDataList;
        }

        private void SetInfoAboutLogFile()
        {
            NotesFromCarriageWithSoftStartup = Convert.ToString(tableDataCounter + eventCounter);
            NumericDataCount = Convert.ToString(tableDataCounter);
            EventsDataCount = Convert.ToString(eventCounter);

            SetDatePeriod(tableDataList[0], tableDataList[tableDataList.Count - 1], eventDataList[0], eventDataList[eventDataList.Count - 1]);
        }

        private void SetDatePeriod(TableData startDate, TableData finishDate, EventData startEventData, EventData finishEventData)
        {
            DateTime absoluteStartDate = startDate.Date > startEventData.Date ? startEventData.Date : startDate.Date;
            DateTime absoluteFinishDate = finishDate.Date < finishEventData.Date ? finishEventData.Date : finishDate.Date;

            DatePeriod = "Период:  с " + absoluteStartDate   + " по " + absoluteFinishDate;
        }

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
            byte[] dateOfFirstMessage = { request[8], request[9], request[10], request[11] };
            byte[] dateOfLastMessage = { request[12], request[13], request[14], request[15] };

            byte eventIdentifier = 65;

            int date2 = BitConverter.ToInt32(dateOfFirstMessage, 0);
            int date3 = BitConverter.ToInt32(dateOfLastMessage, 0);

            TableDataDate = SetDateOfMessageSending(UnixTimeStampToDateTime(date2), UnixTimeStampToDateTime(date3));

            int CountOfMessagesInRequest = 0;

            for (int i = 19; i < request.Length; )
            {
                if (request[i] == 88 && request[i + 1] == 49)
                {
                    if (TableDataDate.Count > CountOfMessagesInRequest)
                    {
                        DateOfMessageInRequest = TableDataDate[CountOfMessagesInRequest];
                        CountOfMessagesInRequest++;
                    }
                    else
                    {
                        DateOfMessageInRequest = TableDataDate[CountOfMessagesInRequest - 1].AddTicks(100);
                        CountOfMessagesInRequest++;
                    }

                    TableData tbl = new TableData();
                    ParseTableDataFromLogFile(tbl, request, i);
                    listTable.Add(tbl);
                    i += 23;
                }
                else if (request[i - 2] == eventIdentifier)
                {
                    string infoAboutEvent = Encoding.Default.GetString(request, i, 64);

                    EventData emp = new EventData();
                    ParseEventDataFromLogFile(emp, infoAboutEvent);
                    listEvent.Add(emp);
                    i += 67;
                }
                else
                {
                    i = request.Length;
                }
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

        public DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1980, 1, 1, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        public void ParseTableDataFromLogFile(TableData e, byte[] request, int i)
        {
            byte[] VoltageA = { request[i + 2], request[i + 3] };
            byte[] VoltageB = { request[i + 4], request[i + 5] };
            byte[] VoltageC = { request[i + 6], request[i + 7] };

            byte[] AmperageA = { request[i + 8], request[i + 9] };
            byte[] AmperageB = { request[i + 10], request[i + 11] };
            byte[] AmperageC = { request[i + 12], request[i + 13] };

            byte[] Loil = { request[i + 14], request[i + 15] };
            byte[] Toil = { request[i + 16], request[i + 17] };

            byte Poil = request[i + 18];
            byte Temperature = request[i + 19];
           
            e.ID = tableDataCounter;

            e.Date = DateOfMessageInRequest;

            e.VoltageA = BitConverter.ToInt16(VoltageA, 0);
            e.VoltageB = BitConverter.ToInt16(VoltageB, 0);
            e.VoltageC = BitConverter.ToInt16(VoltageC, 0);

            e.AmperageA = BitConverter.ToInt16(AmperageA, 0); 
            e.AmperageB = BitConverter.ToInt16(AmperageB, 0); 
            e.AmperageC = BitConverter.ToInt16(AmperageC, 0); 

            e.Loil = BitConverter.ToInt16(Loil, 0); 
            e.Toil = BitConverter.ToInt16(Toil, 0);

            e.Poil = Convert.ToInt32(Poil); 
            e.ThyristorTemperature = Convert.ToInt32(Temperature);

            tableDataCounter++;
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
                string dateTime = Convert.ToString(eventDateMatch);
                eventCounter++;

                e.ID = eventCounter;
                e.Message = Convert.ToString(messageDataMatch);
                e.Date = DateTime.Parse(dateTime);
                if (Convert.ToString(statusDataMatch) == "")
                {
                    e.Status = "C";
                }
                else 
                {
                    e.Status = Convert.ToString(statusDataMatch);
                }
            }
            else
            {
                //Console.WriteLine("Don't find any values");
            }
        }

        private void OpenTableViewClick()
        {
            var vm = new TableDataViewModel(EventDataList, TableDataList, EventJoinedWithTableDataList);
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
