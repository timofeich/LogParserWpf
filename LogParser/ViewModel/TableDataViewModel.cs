using LogParser.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogParser.ViewModel
{
    public class TableDataViewModel
    {
        public event EventHandler OnRequestClose;

        public List<List<int>> AllLogFileData;

        public List<List<string>> AllEventDataInLogFile;
        public ObservableCollection<EventData> Events { get; set; }
        public ObservableCollection<TableData> Tables { get; set; }

        public TableDataViewModel(List<List<int>> AllLogFileData, List<List<string>> AllEventDataInLogFile)
        {
            this.AllLogFileData = AllLogFileData;
            this.AllEventDataInLogFile = AllEventDataInLogFile;

            OutputTableData();
            OutputEventData();
        }

        private void OutputTableData()
        {
            Tables = new ObservableCollection<TableData>();

            for (int j = 0; j < AllLogFileData[0].Count; j++)
            {
                Tables.Add(new TableData()
                {
                    TimeOfRequest = Convert.ToString(LogFile.UnixTimeStampToDateTime(AllLogFileData[0][j])),

                    VoltageA = AllLogFileData[1][j],
                    VoltageB = AllLogFileData[2][j],
                    VoltageC = AllLogFileData[3][j],

                    AmperageA = AllLogFileData[4][j],
                    AmperageB = AllLogFileData[5][j],
                    AmperageC = AllLogFileData[6][j],

                    Loil = AllLogFileData[7][j],
                    Toil = AllLogFileData[8][j],
                    Poil = AllLogFileData[9][j],

                    ThyristorTemperature = AllLogFileData[10][j],
                });
            }
        }

        private void OutputEventData()
        {
            Events = new ObservableCollection<EventData>();

            for (int j = 0; j < AllEventDataInLogFile[0].Count; j++)
            {
                Events.Add(new EventData()
                {
                    MessageID = j,
                    TextMessage = AllEventDataInLogFile[0][j],
                    EventStatus = AllEventDataInLogFile[1][j],
                    MessageDate = AllEventDataInLogFile[2][j]
                });
            }
        }
    }
}
