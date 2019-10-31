using LogParser.Model;
using System;
using System.Collections.ObjectModel;

namespace LogParser.ViewModel
{
    public class TableDataViewModel
    {
        public event EventHandler OnRequestClose;
        public ObservableCollection<EventData> EventDataList { get; set; }
        public ObservableCollection<TableData> TableDataList { get; set; }
        public ObservableCollection<EventJoinedWithTableData> JoinEventAndTableList { get; set; }

        public TableDataViewModel(ObservableCollection<EventData> EventDataList, ObservableCollection<TableData> TableDataList, 
                                        ObservableCollection<EventJoinedWithTableData> JoinEventAndTableList)
        {
            this.EventDataList = EventDataList;
            this.TableDataList = TableDataList;
            this.JoinEventAndTableList = JoinEventAndTableList;
        }
    }
}
