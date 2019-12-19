using LogParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.DataAccess
{
    public interface IDataService : IDisposable
    {
        TableData GetTableDataById(int tableDataId);

        void SaveTableData(TableData tableData);

        void DeleteTableData(int tableDataId);

        IEnumerable<TableData> GetAllTableData();
    }
}
