using LogParser.Model;

namespace LogParser.ViewModel
{
    class MainWindowViewModel
    {
        public static LogFileInformation LogFileInformation { get; set; }
        public static LogFileType LogFileType { get; set; }

        public MainWindowViewModel()
        {
            LogFileInformation = new LogFileInformation { };
            LogFileType = new LogFileType { };

            TestLogFileInformation();
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

        public void TestLogFileType()
        {

        }


    }
}
