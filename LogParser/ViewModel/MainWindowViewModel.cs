﻿using LogParser.Model;
using LogParser.View;
using System;
using System.Windows.Input;

namespace LogParser.ViewModel
{
    class MainWindowViewModel
    {
        public static LogFileInformation LogFileInformation { get; set; }

        LogFile logFile = new LogFile();
        public ICommand OpenCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand OpenTableViewCommand { get; set; }
        public ICommand OpenEventViewCommand { get; set; }
        public ICommand OpenTableAndEventViewCommand { get; set; }

        public MainWindowViewModel()
        {
            InitializeStartupData();
            BindingCommandsToClickMethods();
        }

        private void InitializeStartupData()
        {
            LogFileInformation = new LogFileInformation { };
            LogFileInformation.IsFileOpened = false;
        }

        private void BindingCommandsToClickMethods()
        {
            OpenCommand = new Command(arg => OpenClick());
            CloseCommand = new Command(arg => CloseClick());

            OpenTableViewCommand = new Command(arg => OpenTableViewClick());
            OpenEventViewCommand = new Command(arg => OpenEventViewClick());
            OpenTableAndEventViewCommand = new Command(arg => OpenTableAndEventViewClick());
        }

        private void SetFileName(string fileName)
        {
            LogFileInformation.FileName = "Имя файла:  " + fileName;
        }

        private void SetDatePeriod(DateTime startDate, DateTime finishDate)
        {
            LogFileInformation.DatePeriod = "Период:  с " + startDate + " по " + finishDate;
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

        private void OpenClick()
        {
            logFile.Open();
            if (logFile.IsOpened)
            {
                SetFileName(logFile.FileName);

                SetDatePeriod(logFile.StartDate, logFile.FinishDate);

                SetNotesFromCarriageWithSoftStartup(logFile.NumberOfRecordsFromCarriageWithSoftStartup);

                SetNumberOfNumericData(logFile.NumberOfRecordsWithNumericData);
                SetNumberOfEventsData(logFile.NumberOfRecordsWithEventsData);

                LogFileInformation.IsFileOpened = true;
            }
        }

        private void CloseClick()
        {
            LogFileInformation.IsFileOpened = false;
        }

        private void OpenTableViewClick()
        {
            var vm = new TableDataViewModel(logFile.AllDataFromLogFile);
            var connectSettingView = new TableDataView
            {
                DataContext = vm
            };
            vm.OnRequestClose += (s, e) => connectSettingView.Close();
            connectSettingView.ShowDialog();
        }

        private void OpenEventViewClick()
        {
            var vm = new EventDataViewModel(logFile.AllEventsFromLogFile);
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
