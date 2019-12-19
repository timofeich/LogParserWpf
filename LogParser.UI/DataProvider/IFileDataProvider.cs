using LogParser.Model;
using System.Collections.Generic;

namespace LogParser.UI.DataProvider
{
    public interface IFileDataProvider
    {
        IEnumerable<TableData> GetAllTableData();
    }
}
