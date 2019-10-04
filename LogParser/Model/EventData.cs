using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.Model
{
    public class EventData : INotifyPropertyChanged
    {
        private int messageID;
        public int MessageID
        {
            get { return messageID; }
            set
            {
                if (messageID != value)
                {
                    messageID = value;
                    OnPropertyChanged("MessageID");
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
