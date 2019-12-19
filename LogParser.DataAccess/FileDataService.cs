using LogParser.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.DataAccess
{
    public class FileDataService : IDataService
    {
        private const string StorageFile = "LOG011.json";

        public TableData GetTableDataById(int tableDataId)
        {
            var tableDatas = ReadFromFile();
            return tableDatas.Single(f => f.Id == tableDataId);
        }

        public void SaveTableData(TableData tableData)
        {
            if (tableData.Id <= 0)
            {
                InsertTableData(tableData);
            }
            else
            {
                UpdateTableData(tableData);
            }
        }

        public void DeleteTableData(int tableDataId)
        {
            var tableDatas = ReadFromFile();
            var existing = tableDatas.Single(f => f.Id == tableDataId);
            tableDatas.Remove(existing);
            SaveToFile(tableDatas);
        }

        private void UpdateTableData(TableData tableData)
        {
            var tableDatas = ReadFromFile();
            var existing = tableDatas.Single(f => f.Id == tableData.Id);
            var indexOfExisting = tableDatas.IndexOf(existing);
            tableDatas.Insert(indexOfExisting, tableData);
            tableDatas.Remove(existing);
            SaveToFile(tableDatas);
        }

        private void InsertTableData(TableData tableData)
        {
            var tableDatas = ReadFromFile();
            var maxFriendId = tableDatas.Count == 0 ? 0 : tableDatas.Max(f => f.Id);
            tableData.Id = maxFriendId + 1;
            tableDatas.Add(tableData);
            SaveToFile(tableDatas);
        }

        public IEnumerable<TableData> GetAllTableData()
        {
            return ReadFromFile();
        }

        public void Dispose()
        {
            // Usually Service-Proxies are disposable. This method is added as demo-purpose
            // to show how to use an IDisposable in the client with a Func<T>. =>  Look for example at the FriendDataProvider-class
        }

        private void SaveToFile(List<TableData> friendList)
        {
            string json = JsonConvert.SerializeObject(friendList, Formatting.Indented);
            File.WriteAllText(StorageFile, json);
        }

        private List<TableData> ReadFromFile()
        {

            if (!File.Exists(StorageFile))
            {
                return new List<TableData>
                {
                    new TableData {Id = 0, Date = new DateTime(2019, 08, 26, 1, 51, 54), VoltageA = 709, VoltageB = 715,
                        VoltageC = 709, AmperageA = 13, AmperageB = 14, AmperageC = 14, Loil = 380, Poil = 124, Toil = 0,
                        ThyristorTemperature = 255 },
                    new TableData {Id = 1, Date = new DateTime(2019, 08, 26, 1, 51, 55), VoltageA = 710, VoltageB = 713,
                        VoltageC = 708, AmperageA = 13, AmperageB = 14, AmperageC = 14, Loil = 380, Poil = 124, Toil = 0,
                        ThyristorTemperature = 255 },
                    new TableData {Id = 2, Date = new DateTime(2019, 08, 26, 1, 51, 56), VoltageA = 723, VoltageB = 729,
                        VoltageC = 723, AmperageA = 15, AmperageB = 16, AmperageC = 15, Loil = 380, Poil = 124, Toil = 0,
                        ThyristorTemperature = 255 },
                    new TableData {Id = 3, Date = new DateTime(2019, 08, 26, 1, 51, 57), VoltageA = 716, VoltageB = 722,
                        VoltageC = 718, AmperageA = 12, AmperageB = 13, AmperageC = 12, Loil = 380, Poil = 124, Toil = 0,
                        ThyristorTemperature = 255 },
                    new TableData {Id = 4, Date = new DateTime(2019, 08, 26, 1, 51, 58), VoltageA = 692, VoltageB = 696,
                        VoltageC = 691, AmperageA = 40, AmperageB = 41, AmperageC = 40, Loil = 380, Poil = 124, Toil = 0,
                        ThyristorTemperature = 255 },
                };
            }

            string json = File.ReadAllText(StorageFile);
            return JsonConvert.DeserializeObject<List<TableData>>(json);
        }
    }
}
