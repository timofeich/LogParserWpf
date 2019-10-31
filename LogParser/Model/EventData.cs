using System;

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
