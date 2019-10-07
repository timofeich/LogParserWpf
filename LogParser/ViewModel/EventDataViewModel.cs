using LogParser.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.ViewModel
{
    public class EventDataViewModel : INotifyPropertyChanged 
    {
        public event EventHandler OnRequestClose;
        string[] LogFileContent;
        public ObservableCollection<EventData> Events { get; set; }

        public EventDataViewModel()
        {
            
            
        }

        public EventDataViewModel(string[] LogFileContent)
        {
            Events = new ObservableCollection<EventData>
            {
                new EventData { MessageID = 0, TextMessage = "123", MessageDate = "Steven" },
                new EventData { MessageID = 1, TextMessage = "321", MessageDate = "John"},
            };
            this.LogFileContent = LogFileContent;
            OutputEventData();
        }

        private void OutputEventData()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
