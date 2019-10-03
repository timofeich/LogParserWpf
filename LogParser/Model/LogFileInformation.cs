using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private string dataPeriod;
        public string DataPeriod
        {
            get { return dataPeriod; }
            set
            {
                if (dataPeriod != value)
                {
                    dataPeriod = value;
                    OnPropertyChanged("DataPeriod");
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
                    dataPeriod = value;
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
                    dataPeriod = value;
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
                    dataPeriod = value;
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
                    dataPeriod = value;
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
