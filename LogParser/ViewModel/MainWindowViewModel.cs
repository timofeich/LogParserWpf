using LogParser.Model;
using System;
using System.Windows.Input;

namespace LogParser.ViewModel
{
    class MainWindowViewModel
    {
        public static LogFileInformation LogFileInformation { get; set; }
        public static LogFileType LogFileType { get; set; }

        public ICommand OpenCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand OpenTableCommand { get; set; }

        public MainWindowViewModel()
        {
            LogFileInformation = new LogFileInformation { };
            LogFileType = new LogFileType { };

            TestLogFileInformation();
            BindingCommandsToClickMethods();
        }

        public void TestLogFileInformation()
        {
            LogFileInformation.FileName = "1";
            LogFileInformation.DataPeriod = "2";
            LogFileInformation.NotesFromCarriageWithVariableFrequencyDrive = "3";
            LogFileInformation.NotesFromCarriageWithSoftStartup = "4";
            LogFileInformation.NumericData = "5";
            LogFileInformation.EventsData = "6";
        }

        private void BindingCommandsToClickMethods()
        {
            OpenCommand = new Command(arg => OpenClick());
            CloseCommand = new Command(arg => CloseClick());
            SaveCommand = new Command(arg => SaveClick());
            OpenTableCommand = new Command(arg => OpenTableClick());
        }

        private void OpenClick()
        {
            throw new NotImplementedException();
        }

        private void CloseClick()
        {
            throw new NotImplementedException();
        }

        private void SaveClick()
        {
            throw new NotImplementedException();
        }

        private void OpenTableClick()
        {
            throw new NotImplementedException();
        }

    }
}
