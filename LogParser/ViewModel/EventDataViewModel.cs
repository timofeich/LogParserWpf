using LogParser.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogParser.ViewModel
{
    public class EventDataViewModel 
    {
        public event EventHandler OnRequestClose;
        List<List<string>> AllEventDataInLogFile;
        public ObservableCollection<EventData> Events { get; set; }

        public EventDataViewModel(List<List<string>> AllEventDataInLogFile)
        {
            this.AllEventDataInLogFile = AllEventDataInLogFile;
            OutputEventData();
        }

        private void OutputEventData()
        {
            Events = new ObservableCollection<EventData>();

            for (int j = 0; j < AllEventDataInLogFile[0].Count; j++)
            {
                Events.Add(new EventData()
                {
                    MessageID = j,
                    TextMessage = AllEventDataInLogFile[0][j],
                    EventStatus = AllEventDataInLogFile[1][j]
                });
            }
        }
    }
}
