using LogParser.Model;
using System;
using System.Collections.ObjectModel;

namespace LogParser.ViewModel
{
    public class EventDataViewModel 
    {
        public event EventHandler OnRequestClose;
        string[] LogFileContent;
        public ObservableCollection<EventData> Events { get; set; }

        public EventDataViewModel(string[] LogFileContent)
        {
            Events = new ObservableCollection<EventData>
            {
                new EventData { MessageID = 0, TextMessage = "123", MessageDate = "Steven" },
                new EventData { MessageID = 1, TextMessage = "321", MessageDate = "John" },
            };

            this.LogFileContent = LogFileContent;

            //EventData event1 = new EventData();          
            //OutputEventData(event1);
        }

        private void OutputEventData(EventData eventData)
        {
            eventData.MessageID = 2;
            eventData.TextMessage = LogFileContent[3];
            eventData.MessageDate = LogFileContent[0];
            Events.Add(eventData);
        }
    }
}
