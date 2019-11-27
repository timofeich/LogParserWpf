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
        public bool IsLogFileFromCarriage { get; set; }

        public TableDataViewModel(ObservableCollection<EventData> EventDataList, ObservableCollection<TableData> TableDataList, 
                                        ObservableCollection<EventJoinedWithTableData> JoinEventAndTableList, bool IsLogFileFromCarriage)
        {
            this.EventDataList = EventDataList;
            this.TableDataList = TableDataList;
            this.JoinEventAndTableList = JoinEventAndTableList;
            this.IsLogFileFromCarriage = IsLogFileFromCarriage;
        }
    }
}
