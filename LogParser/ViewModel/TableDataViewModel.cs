using LogParser.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogParser.ViewModel
{
    public class TableDataViewModel
    {
        public event EventHandler OnRequestClose;

        public List<List<int>> AllLogFileData;

        public ObservableCollection<TableData> Tables { get; set; }

        public TableDataViewModel(List<List<int>> AllLogFileData)
        {
            this.AllLogFileData = AllLogFileData;
            OutputEventData();
        }

        private void OutputEventData()
        {
            Tables = new ObservableCollection<TableData>();

            for (int j = 0; j < AllLogFileData[0].Count; j++)
            {
                Tables.Add(new TableData()
                {
                    VoltageA = AllLogFileData[0][j],
                    VoltageB = AllLogFileData[1][j],
                    VoltageC = AllLogFileData[2][j],

                    AmperageA = AllLogFileData[3][j],
                    AmperageB = AllLogFileData[4][j],
                    AmperageC = AllLogFileData[5][j],

                    Loil = AllLogFileData[6][j],
                    Toil = AllLogFileData[7][j],
                    Poil = AllLogFileData[8][j],

                    ThyristorTemperature = AllLogFileData[9][j],
                });
            }

        }
    }
}
