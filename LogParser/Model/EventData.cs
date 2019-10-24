using System;
using System.ComponentModel;

namespace LogParser.Model
{
    public class EventData
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
