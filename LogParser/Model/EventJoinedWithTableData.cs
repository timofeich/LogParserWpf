using System;
using System.Collections.ObjectModel;

namespace LogParser.Model
{
    public class EventJoinedWithTableData
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        //public ObservableCollection<EventData> EventDatas { get; set; }
        public ObservableCollection<TableData> TableDatas { get; set; }
    }
}
