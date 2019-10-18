using LogParser.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogParser.ViewModel
{
    public class TableDataViewModel
    {
        public event EventHandler OnRequestClose;

        public List<int> LogFileContent;
        public List<TableData> Tables { get; set; }

        public TableDataViewModel(List<int> LogFileContent)
        {
            this.LogFileContent = LogFileContent;
            OutputEventData();
        }

        private void OutputEventData()
        {
            Tables = new List<TableData>();
            for (int i = 0; i < LogFileContent.Count; i++)
            {
                Tables.Add(new TableData() { VoltageA = LogFileContent[i] });
            }
        }
    }
}
