using LogParser.DataParse;
using LogParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Documents;

namespace LogParser.DataAccess
{
    public class FileDataService : IDataService
    {
        public string StorageFile = "D:\\LOG011";

        private FileDataParsing logFile;
        public FileDataService()
        {
            if (File.Exists(StorageFile))
            {
                logFile = new FileDataParsing(StorageFile);
                logFile.ParseLogFile();
            }
            else
            {

            }
        }

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

        public FileInformation GetAllInformationAboutFile()
        {
            return logFile.fileInformation;
        }

        private List<TableData> ReadFromFile()
        {
            return logFile.listTable;
        }

        private List<EventJoinedWithTableData> ReadEventJoinedWithTableDataFromFile()
        {
            return logFile.listJoin;
        }

        private List<EventData> ReadEventFromFile()
        {
            return logFile.listEvent;
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


    }
}
