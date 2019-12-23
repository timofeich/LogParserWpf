using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.Model
{
    public class EventJoinedWithTableData
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public List<TableData> TableDatas { get; set; }
    }
}
