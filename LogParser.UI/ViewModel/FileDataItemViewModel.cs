using LogParser.UI.Command;
using LogParser.UI.Events;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogParser.UI.ViewModel
{
    public class FileDataItemViewModel
    {
        public FileDataItemViewModel(int id, string message, DateTime date, 
            string status, IEventAggregator eventAggregator)
        {
            Id = id;
            Message = message;
            Date = date;
            Status = status;
            OpenFileDataEditViewCommand = new DelegateCommand(OnFileDataEditViewExecute);
            _eventAggregator = eventAggregator;
        }

        private void OnFileDataEditViewExecute(object obj)
        {
            _eventAggregator.GetEvent<OpenTableDataEditViewEvent>()
                .Publish(Id);
        }

        public int Id { get; private set; }
        public string Message { get; private set; }
        public DateTime Date { get; private set; }
        public string Status { get; private set; }
        public ICommand OpenFileDataEditViewCommand { get; private set; }

        private IEventAggregator _eventAggregator;
    }
}
