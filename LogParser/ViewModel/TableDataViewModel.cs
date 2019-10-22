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

        public ObservableCollection<TableData> Tables { get; set; }

        public TableDataViewModel(List<List<int>> AllLogFileData)
        {
            this.AllLogFileData = AllLogFileData;
            OutputTableData();
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
    }
}
