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
        private FileDataParsing logFile;
        public FileDataService()
        {
            logFile = new FileDataParsing();
        }

        public object FileDataParsing { get; private set; }

        public TableData GetTableDataById(int tableDataId)
        {
            var tableDatas = ReadFromFile();
            return tableDatas.Single(f => f.Id == tableDataId);
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
    }
}
