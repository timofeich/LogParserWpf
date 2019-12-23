using LogParser.DataParse;
using LogParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LogParser.DataAccess
{
    public class FileDataService : IDataService
    {
        private const string StorageFile = "D:\\LOG011";

        public object FileDataParsing { get; private set; }

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

        public IEnumerable<EventData> GetAllEventData()
        {
            return ReadEventFromFile();
        }

        public IEnumerable<EventJoinedWithTableData> GetAllEventJoinedWithTableData()
        {
            return ReadEventJoinedWithTableDataFromFile();
        }

        private IEnumerable<EventJoinedWithTableData> ReadEventJoinedWithTableDataFromFile()
        {
            if (File.Exists(StorageFile))
            {
                FileDataParsing logFile = new FileDataParsing(StorageFile);
                return logFile.listJoin;
            }
            else
            {
                return null;
            }
        }

        private IEnumerable<EventData> ReadEventFromFile()
        {
            if (File.Exists(StorageFile))
            {
                FileDataParsing logFile = new FileDataParsing(StorageFile);
                return logFile.listEvent;
            }
            else
            {
                return null;
            }
        }

        public void Dispose()
        {
            // Usually Service-Proxies are disposable. This method is added as demo-purpose
            // to show how to use an IDisposable in the client with a Func<T>. =>  Look for example at the FriendDataProvider-class
        }

        private void SaveToFile(List<TableData> friendList)
        {
            //string json = JsonConvert.SerializeObject(friendList, Formatting.Indented);
            //File.WriteAllText(StorageFile, json);
        }

        private List<TableData> ReadFromFile()
        {
            if (File.Exists(StorageFile))
            {
                FileDataParsing logFile = new FileDataParsing(StorageFile);
                return logFile.listTable;
            }
            else 
            { 
                return null; 
            }
        }
    }
}
