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
    }
}
