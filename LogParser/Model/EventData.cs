﻿using System.ComponentModel;

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

        private string textMessage;
        public string TextMessage
        {
            get { return textMessage; }
            set
            {
                if (textMessage != value)
                {
                    textMessage = value;
                    OnPropertyChanged("TextMessage");
                }
            }
        }

        private string messageDate;
        public string MessageDate
        {
            get { return messageDate; }
            set
            {
                if (messageDate != value)
                {
                    messageDate = value;
                    OnPropertyChanged("MessageDate");
                }
            }
        }

        private string eventStatus;
        public string EventStatus
        {
            get { return eventStatus; }
            set
            {
                if (eventStatus != value)
                {
                    eventStatus = value;
                    OnPropertyChanged("EventStatus");
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
