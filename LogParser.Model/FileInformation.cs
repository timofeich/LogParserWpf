using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.Model
{
    public class FileInformation
    {
        public string FileName { get; set; }
        public string ShortFileName { get; set; }
        public DateTime FirstMessageDate { get; set; }
        public DateTime LastMessageDate { get; set; }
        public int MessagesCount { get; set; }
        public int StandMessagesCount { get; set; }
        public int NumericMessagesCount { get; set; }
        public int EventMessagesCount { get; set; }
    }
}
