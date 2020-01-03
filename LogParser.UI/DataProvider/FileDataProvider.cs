using LogParser.DataAccess;
using LogParser.Model;
using System;
using System.Collections.Generic;

namespace LogParser.UI.DataProvider
{
    class FileDataProvider : IFileDataProvider
    {
        private Func<IDataService> _dataServiceCreator;

        public FileDataProvider(Func<IDataService> dataServiceCreator)
        {
            _dataServiceCreator = dataServiceCreator;
        }

        public IEnumerable<TableData> GetAllTableData()
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetAllTableData();
            }
        }

        public IEnumerable<EventData> GetAllEventData()
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetAllEventData();
            }
        }

        public IEnumerable<EventJoinedWithTableData> GetAllEventJoinedWithTableData()
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetAllEventJoinedWithTableData();
            }
        }

        public FileInformation GetAllInformationAboutFile()
        {
            using (var dataService = _dataServiceCreator())
            {
                return dataService.GetAllInformationAboutFile();
            }
        }
    }
}
