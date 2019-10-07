using LogParser.Model;
using System;
using System.Collections.ObjectModel;

namespace LogParser.ViewModel
{
    public class TableDataViewModel
    {
        public event EventHandler OnRequestClose;
        string[] LogFileContent;
        public ObservableCollection<TableData> Tables { get; set; }

        public TableDataViewModel(string[] LogFileContent)
        {
            Tables = new ObservableCollection<TableData>{ };

            this.LogFileContent = LogFileContent;

            TableData table1 = new TableData();
            OutputEventData(table1);
        }

        private void OutputEventData(TableData tableData)
        {
            tableData.TimeOfRequest = LogFileContent[0];
            tableData.VoltageA = Convert.ToInt32(LogFileContent[4]);
            tableData.VoltageB = Convert.ToInt32(LogFileContent[5]);
            tableData.VoltageC = Convert.ToInt32(LogFileContent[6]);
            tableData.AmperageA = Convert.ToInt32(LogFileContent[7]);
            tableData.AmperageB = Convert.ToInt32(LogFileContent[8]);
            tableData.AmperageC = Convert.ToInt32(LogFileContent[9]);
            tableData.Loil = Convert.ToInt32(LogFileContent[10]);
            tableData.Toil = Convert.ToInt32(LogFileContent[11]);
            tableData.Poil = Convert.ToInt32(LogFileContent[12]);
            tableData.ThyristorTemperature = Convert.ToInt32(LogFileContent[13]);   

            Tables.Add(tableData);
        }
    }
}
