using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LogParser.Model
{
    public class TableData
    {
        public DateTime Date { get; set; }
        public int VoltageA { get; set; }
        public int VoltageB { get; set; }
        public int VoltageC { get; set; }
        public int AmperageA { get; set; }
        public int AmperageB { get; set; }
        public int AmperageC { get; set; }
        public int Loil { get; set; }
        public int Toil { get; set; }
        public int Poil { get; set; }
        public int ThyristorTemperature { get; set; }

    }
}
