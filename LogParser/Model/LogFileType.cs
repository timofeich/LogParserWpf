using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
