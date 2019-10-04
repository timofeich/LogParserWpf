using LogParser.Model;
using LogParser.View;
using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace LogParser.ViewModel
{
    class MainWindowViewModel
    {
        public static LogFileInformation LogFileInformation { get; set; }
        public static LogFileType LogFileType { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand OpenTableViewCommand { get; set; }
        public ICommand OpenEventViewCommand { get; set; }
        public ICommand OpenTableAndEventViewCommand { get; set; }

        public MainWindowViewModel()
        {
            LogFileInformation = new LogFileInformation { };
            LogFileType = new LogFileType { };
            TestLogFileInformation();
            BindingCommandsToClickMethods();

            LogFileType.IsCarriageWithSoftStartup = true;
        }

        public void TestLogFileInformation()
        {
            //SetFileName("FileName");
            //SetDatePeriod("2019.08.26 01:53:54", "2019.08.30 12:00:00");
            //SetNotesFromCarriageWithVariableFrequencyDrive("3");
            //SetNotesFromCarriageWithSoftStartup("4");
            //SetNumberOfNumericData("5");
            //SetNumberOfEventsData("6");
        }

        private void SetFileName(string fileName)
        {
            LogFileInformation.FileName = "Имя файла:  " + fileName;
        }

        private void SetDatePeriod(string startDate, string finishDate)
        {
            LogFileInformation.DatePeriod = "Период:  с " + startDate + " по " + finishDate;
        }

        private void SetNotesFromCarriageWithVariableFrequencyDrive(string notesCount)
        {
            LogFileInformation.NotesFromCarriageWithVariableFrequencyDrive = "Записей от вагона с частотно регулируемым " +
                "приводом (ПЧ):  " + notesCount;
        }

        private void SetNotesFromCarriageWithSoftStartup(string notesCount)
        {
            LogFileInformation.NotesFromCarriageWithSoftStartup = "Записей от вагона с устройством плавного " +
                "пуска (УПП):  " + notesCount;
        }

        private void SetNumberOfNumericData(string numberOfNumericData)
        {
            LogFileInformation.NumericData = "Из них с числовыми данными:  " + numberOfNumericData;
        }

        private void SetNumberOfEventsData(string numberOfEventsData)
        {
            LogFileInformation.EventsData = "Из них с событиями:  " + numberOfEventsData;
        }

        private void BindingCommandsToClickMethods()
        {
            OpenCommand = new Command(arg => OpenClick());
            CloseCommand = new Command(arg => CloseClick());
            SaveCommand = new Command(arg => SaveClick());

            OpenTableViewCommand = new Command(arg => OpenTableViewClick());
            OpenEventViewCommand = new Command(arg => OpenEventViewClick());
            OpenTableAndEventViewCommand = new Command(arg => OpenTableAndEventViewClick());
        }

        private void OpenClick()
        {
            LogFile logFile = new LogFile();
            SetFileName(logFile.FileName);
            SetNumberOfNumericData(logFile.Length);
            SetNumberOfEventsData(logFile.Result[7]);
        }

        private void CloseClick()
        {
            throw new NotImplementedException();
        }

        private void SaveClick()
        {
            throw new NotImplementedException();
        }

        private void OpenTableViewClick()
        {
            var vm = new TableDataViewModel();
            var tableDataView = new TableDataView
            {
                DataContext = vm
            };
            vm.OnRequestClose += (s, e) => tableDataView.Close();
            tableDataView.ShowDialog();
        }

        private void OpenEventViewClick()
        {
            var vm = new EventDataViewModel();
            var eventDataView = new EventDataView
            {
                DataContext = vm
            };
            vm.OnRequestClose += (s, e) => eventDataView.Close();
            eventDataView.ShowDialog();
        }

        private void OpenTableAndEventViewClick()
        {
            var vm = new TableAndEventDataViewModel();
            var tableAndEventDataView = new TableAndEventDataView
            {
                DataContext = vm
            };
            vm.OnRequestClose += (s, e) => tableAndEventDataView.Close();
            tableAndEventDataView.ShowDialog();
        }
    }
}
