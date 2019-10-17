using LogParser.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogParser.ViewModel
{
    public class TableDataViewModel
    {
        public event EventHandler OnRequestClose;
        List<int> LogFileContent;
        public ObservableCollection<TableData> Tables { get; set; }

        public TableDataViewModel(List<int> LogFileContent)
        {
            this.LogFileContent = LogFileContent;

            OutputEventData();
        }

        private void OutputEventData()
        {
            Tables = new ObservableCollection<TableData>();
            for (int i = 0; i < LogFileContent.Count; i++)
            {
                Tables.Add(new TableData() { VoltageA = LogFileContent[i]});
            }

            //tableData.TimeOfRequest = LogFileContent[0];
            //tableData.VoltageA = Convert.ToInt32(LogFileContent[4]);
            //tableData.VoltageB = Convert.ToInt32(LogFileContent[5]);
            //tableData.VoltageC = Convert.ToInt32(LogFileContent[6]);
            //tableData.AmperageA = Convert.ToInt32(LogFileContent[7]);
            //tableData.AmperageB = Convert.ToInt32(LogFileContent[8]);
            //tableData.AmperageC = Convert.ToInt32(LogFileContent[9]);
            //tableData.Loil = Convert.ToInt32(LogFileContent[10]);
            //tableData.Toil = Convert.ToInt32(LogFileContent[11]);
            //tableData.Poil = Convert.ToInt32(LogFileContent[12]);
            //tableData.ThyristorTemperature = Convert.ToInt32(LogFileContent[13]);   

            //Tables.Add(tableData);
        }
    }
}
