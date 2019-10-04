using System.ComponentModel;

namespace LogParser.Model
{
    public class LogFileInformation : INotifyPropertyChanged
    {
        private string fileName;
        public string FileName
        {
            get { return fileName; }
            set
            {
                if (fileName != value)
                {
                    fileName = value;
                    OnPropertyChanged("FileName");
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
                    OnPropertyChanged("DatePeriod");
                }
            }
        }

        private string notesFromCarriageWithVariableFrequencyDrive;
        public string NotesFromCarriageWithVariableFrequencyDrive
        {
            get { return notesFromCarriageWithVariableFrequencyDrive; }
            set
            {
                if (notesFromCarriageWithVariableFrequencyDrive != value)
                {
                    notesFromCarriageWithVariableFrequencyDrive = value;
                    OnPropertyChanged("NotesFromCarriageWithVariableFrequencyDrive");
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
                    notesFromCarriageWithSoftStartup = value;
                    OnPropertyChanged("NotesFromCarriageWithSoftStartup");
                }
            }
        }

        private string numericData;
        public string NumericData
        {
            get { return numericData; }
            set
            {
                if (numericData != value)
                {
                    numericData = value;
                    OnPropertyChanged("NumericData");
                }
            }
        }

        private string eventsData;
        public string EventsData
        {
            get { return eventsData; }
            set
            {
                if (eventsData != value)
                {
                    eventsData = value;
                    OnPropertyChanged("EventsData");
                }
            }
        }

        #region Implement INotifyPropertyChanged members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
