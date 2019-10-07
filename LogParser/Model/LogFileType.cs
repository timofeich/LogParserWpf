﻿using System.ComponentModel;

namespace LogParser.Model
{
    public class LogFileType : INotifyPropertyChanged
    {
        private bool isCarriageWithVariableFrequencyDrive;
        public bool IsCarriageWithVariableFrequencyDrive
        {
            get { return isCarriageWithVariableFrequencyDrive; }
            set
            {
                if (isCarriageWithVariableFrequencyDrive != value)
                {
                    isCarriageWithVariableFrequencyDrive = value;
                    OnPropertyChanged("IsCarriageWithVariableFrequencyDrive");
                }
            }
        }

        private bool isCarriageWithSoftStartup;
        public bool IsCarriageWithSoftStartup
        {
            get { return isCarriageWithSoftStartup; }
            set
            {
                if (isCarriageWithSoftStartup != value)
                {
                    isCarriageWithSoftStartup = value;
                    OnPropertyChanged("IsCarriageWithSoftStartup");
                }
            }
        }

        private bool isFileOpened;
        public bool IsFileOpened
        {
            get { return isFileOpened; }
            set
            {
                if(isFileOpened != value)
                {
                    isFileOpened = value;
                    OnPropertyChanged("IsFileOpened");
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
