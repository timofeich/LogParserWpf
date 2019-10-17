using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace LogParser.ViewModel
{
    public class LogFile
    {
        public string FileName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string NumberOfRecordsFromCarriageWithVariableFrequencyDrive { get; set; }
        public string NumberOfRecordsFromCarriageWithSoftStartup { get; set; }
        public string NumberOfRecordsWithNumericData { get; set; }
        public string NumberOfRecordsWithEventsData { get; set; }
        public List<int> ListOfVoltageA = new List<int>(); 
        public List<int> ListOfVoltageB { get; set; }
        public List<int> ListOfVoltageC { get; set; }

        private int EventCounter = 0;
        private int NumericDataCounter = 0;

        public string[] Result;
        public bool IsOpened;

        public LogFile() { }

        public void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Бинарные файлы (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.SafeFileName;
                ParseLogFileByBytesNew(openFileDialog.FileName);

                IsOpened = true;
            }
            else
            {
                IsOpened = false;
            }
        }

        public void ParseLogFileByBytesNew(string fileName)
        {
            List<byte[]> listOfRequests = new List<byte[]>();

            using (BinaryReader b = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                byte[][] DataMatrix = new byte[][] { };

                for (int i = 0; i < b.BaseStream.Length; i += 512)
                {
                    b.BaseStream.Seek(i, SeekOrigin.Begin);
                    if (b.PeekChar() == 'L')
                    {
                        listOfRequests.Add(b.ReadBytes(512));
                    }
                }

                foreach (byte[] request in listOfRequests)
                {
                    ParseRequest(request);
                }
            }
        }

        private void ParseRequest(byte[] request)
        {
            //byte[] requestTitle = { request[0], request[1], request[2], request[3] };

            byte[] dateOfFirstRequest = { request[4], request[5], request[6], request[7] };
            byte[] dateOfFirstMessage = { request[8], request[9], request[10], request[11] };
            byte[] dateOfLastMessage = { request[12], request[13], request[14], request[15] };

            byte eventIdentifier = 65;

            int date1 = ConvertBytesArrayToIntValue(dateOfFirstRequest);
            int date2 = ConvertBytesArrayToIntValue(dateOfFirstMessage);
            int date3 = ConvertBytesArrayToIntValue(dateOfLastMessage);

            //Console.WriteLine(UnixTimeStampToDateTime(date1) + "    " + UnixTimeStampToDateTime(date2) + "   "
            //                + UnixTimeStampToDateTime(date3));

            if (request[19] == 88 && request[20] == 49)
            {
                byte[] VoltageA = { request[21], request[22] };
                byte[] VoltageB = { request[23], request[24] };
                byte[] VoltageC = { request[25], request[26] };

                byte[] Ia = { request[27], request[28] };
                byte[] Ib = { request[29], request[30] };
                byte[] Ic = { request[31], request[32] };

                byte[] Loil = { request[33], request[34] };
                byte[] Toil = { request[35], request[36] };

                byte Poil = request[37];
                byte Temperature = request[38];

                int currentVoltageA = ConvertBytesArrayToInt16Value(VoltageA);

                ListOfVoltageA.Add(currentVoltageA);
                //ListOfVoltageB.Add(ConvertBytesArrayToInt16Value(VoltageB));
                //ListOfVoltageC.Add(ConvertBytesArrayToInt16Value(VoltageC));

                //Console.WriteLine(ConvertBytesArrayToInt16Value(Ua) + "  " + ConvertBytesArrayToInt16Value(Ub) + "   "
                //    + ConvertBytesArrayToInt16Value(Uc) + "   " + ConvertBytesArrayToInt16Value(Ia) + "   " +
                //    ConvertBytesArrayToInt16Value(Ib) + "   " + ConvertBytesArrayToInt16Value(Ic) + "    " +
                //    ConvertBytesArrayToInt16Value(Loil) + "   " + ConvertBytesArrayToInt16Value(Toil) + "   " +
                //    Poil + "  " + Temperature);
            }
            else if (request[17] == eventIdentifier)
            {
                string infoAboutEvent = Encoding.Default.GetString(request, 19, 64);
                ParseEventDataFromLogFile(infoAboutEvent);
            }
            else
            {
                //Console.WriteLine("Unknown request");
            }
        }

        public DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1980, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }

        public int ConvertBytesArrayToIntValue(byte[] byteArray)
        {
            return BitConverter.ToInt32(byteArray, 0);
        }

        public int ConvertBytesArrayToInt16Value(byte[] byteArray)
        {
            return BitConverter.ToInt16(byteArray, 0);
        }

        public void ParseEventDataFromLogFile(string eventData)
        {
            Regex dateInEventData = new Regex(@"\d\d:\d\d:\d\d (\d\d)/(\d\d)/(\d\d)");
            Regex statusInEventData = new Regex(@".{40}");
            Regex messageInEventData = new Regex(@"A|P");

            Match eventDataMatch = dateInEventData.Match(eventData);
            Match statusDataMatch = statusInEventData.Match(eventData);
            Match messageDataMatch = messageInEventData.Match(eventData);

            if (eventDataMatch.Success)
            {
                //Console.WriteLine("Message: " + statusDataMatch + " Date: " + eventDataMatch + " Status: " + messageDataMatch);
            }
            else
            {
                //Console.WriteLine("Don't find any values");
            }
        }    


    }
}
