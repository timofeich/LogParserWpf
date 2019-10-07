using LogParser.Model;
using System;
using System.Collections.ObjectModel;

namespace LogParser.ViewModel
{
    public class TableAndEventDataViewModel
    {
        public event EventHandler OnRequestClose;
        string[] LogFileContent;
        public ObservableCollection<TableAndEventData> TableAndEventDatas { get; set; }

        public TableAndEventDataViewModel(string[] LogFileContent)
        {
            TableAndEventDatas = new ObservableCollection<TableAndEventData> { };

            this.LogFileContent = LogFileContent;

            TableAndEventData tableAndEventData = new TableAndEventData();
            OutputEventData(tableAndEventData);
        }

        private void OutputEventData(TableAndEventData tableAndEventData)
        {
            tableAndEventData.MessageID = 2;
            tableAndEventData.TextMessage = LogFileContent[3];
            tableAndEventData.MessageDate = LogFileContent[0];
            tableAndEventData.TimeOfRequest = LogFileContent[2];
            tableAndEventData.VoltageA = Convert.ToInt32(LogFileContent[4]);
            tableAndEventData.VoltageB = Convert.ToInt32(LogFileContent[5]);
            tableAndEventData.VoltageC = Convert.ToInt32(LogFileContent[6]);
            tableAndEventData.AmperageA = Convert.ToInt32(LogFileContent[7]);
            tableAndEventData.AmperageB = Convert.ToInt32(LogFileContent[8]);
            tableAndEventData.AmperageC = Convert.ToInt32(LogFileContent[9]);
            tableAndEventData.Loil = Convert.ToInt32(LogFileContent[10]);
            tableAndEventData.Toil = Convert.ToInt32(LogFileContent[11]);
            tableAndEventData.Poil = Convert.ToInt32(LogFileContent[12]);
            tableAndEventData.ThyristorTemperature = Convert.ToInt32(LogFileContent[13]);

            TableAndEventDatas.Add(tableAndEventData);
        }

    }
}
