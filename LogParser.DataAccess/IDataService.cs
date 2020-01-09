using LogParser.Model;
using System;
using System.Collections.Generic;

namespace LogParser.DataAccess
{
    public interface IDataService : IDisposable
    {
        TableData GetTableDataById(int tableDataId);

        IEnumerable<TableData> GetAllTableData();

        IEnumerable<EventData> GetAllEventData();

        IEnumerable<EventJoinedWithTableData> GetAllEventJoinedWithTableData();

        FileInformation GetAllInformationAboutFile();

    }
}
