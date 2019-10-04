using LogParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.ViewModel
{
    public class EventDataViewModel
    {
        public event EventHandler OnRequestClose;
        string[] LogFileContent;

        public static EventData EventData { get; set; }

        public EventDataViewModel()
        {
            EventData = new EventData { };
            OutputEventData();
        }

        public EventDataViewModel(string[] LogFileContent)
        {
            this.LogFileContent = LogFileContent;
        }

        private void OutputEventData()
        {
            EventData.MessageID = Convert.ToInt32(LogFileContent[0]);
            EventData.TextMessage = "2";
            EventData.MessageDate = "3";
        }


    }
}
