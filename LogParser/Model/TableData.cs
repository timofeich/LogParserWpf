using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.Model
{
    public class TableData : INotifyPropertyChanged
    {
        private string timeOfRequest;
        public string TimeOfRequest
        {
            get { return timeOfRequest; }
            set
            {
                if (timeOfRequest != value)
                {
                    timeOfRequest = value;
                    OnPropertyChanged("TimeOfRequest");
                }
            }
        }

        private int voltageA;
        public int VoltageA
        {
            get { return voltageA; }
            set
            {
                if (voltageA != value)
                {
                    voltageA = value;
                    OnPropertyChanged("VoltageA");
                }
            }
        }

        private int voltageB;
        public int VoltageB
        {
            get { return voltageB; }
            set
            {
                if (voltageB != value)
                {
                    voltageB = value;
                    OnPropertyChanged("VoltageB");
                }
            }
        }

        private int voltageC;
        public int VoltageC
        {
            get { return voltageC; }
            set
            {
                if (voltageC != value)
                {
                    voltageC = value;
                    OnPropertyChanged("VoltageC");
                }
            }
        }

        private int amperageA;
        public int AmperageA
        {
            get { return amperageA; }
            set
            {
                if (amperageA != value)
                {
                    amperageA = value;
                    OnPropertyChanged("AmperageA");
                }
            }
        }

        private int amperageB;
        public int AmperageB
        {
            get { return amperageB; }
            set
            {
                if (amperageB != value)
                {
                    amperageB = value;
                    OnPropertyChanged("AmperageB");
                }
            }
        }

        private int amperageC;
        public int AmperageC
        {
            get { return amperageC; }
            set
            {
                if (amperageC != value)
                {
                    amperageC = value;
                    OnPropertyChanged("AmperageC");
                }
            }
        }

        private int loil;
        public int Loil
        {
            get { return loil; }
            set
            {
                if (loil != value)
                {
                    loil = value;
                    OnPropertyChanged("Loil");
                }
            }
        }

        private int toil;
        public int Toil
        {
            get { return toil; }
            set
            {
                if (toil != value)
                {
                    toil = value;
                    OnPropertyChanged("Toil");
                }
            }
        }

        private int poil;
        public int Poil
        {
            get { return poil; }
            set
            {
                if (poil != value)
                {
                    poil = value;
                    OnPropertyChanged("Poil");
                }
            }
        }

        private int thytistorTemperature;
        public int ThytistorTemperature
        {
            get { return thytistorTemperature; }
            set
            {
                if (thytistorTemperature != value)
                {
                    thytistorTemperature = value;
                    OnPropertyChanged("ThytistorTemperature");
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
