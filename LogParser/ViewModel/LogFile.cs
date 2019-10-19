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

        public List<List<int>> AllDataFromLogFile = new List<List<int>>();
        public List<List<string>> AllEventsFromLogFile = new List<List<string>>();

        private List<DateTime> DateOfFileCreationList = new List<DateTime>();
        private List<DateTime> DateOfFirstMessageInRequestList = new List<DateTime>();
        private List<DateTime> DateOfLastMessageInRequestList = new List<DateTime>();

        private List<int> VoltageAList = new List<int>();
        private List<int> VoltageBList = new List<int>();
        private List<int> VoltageCList = new List<int>();

        private List<int> AmperageAList = new List<int>();
        private List<int> AmperageBList = new List<int>();
        private List<int> AmperageCList = new List<int>();

        private List<int> LoilList = new List<int>();
        private List<int> ToilList = new List<int>();
        private List<int> PoilList = new List<int>();

        private List<int> TemperatureList = new List<int>();

        private List<string> EventMessageList = new List<string>();
        private List<string> EventStatusList = new List<string>();
 
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

                //AllDataFromLogFile.Add(DateOfFirstMessageInRequestList);
                //AllDataFromLogFile.Add(DateOfLastMessageInRequestList);

                AllDataFromLogFile.Add(VoltageAList);
                AllDataFromLogFile.Add(VoltageBList);
                AllDataFromLogFile.Add(VoltageCList);

                AllDataFromLogFile.Add(AmperageAList);
                AllDataFromLogFile.Add(AmperageBList);
                AllDataFromLogFile.Add(AmperageCList);

                AllDataFromLogFile.Add(LoilList);
                AllDataFromLogFile.Add(ToilList);
                AllDataFromLogFile.Add(PoilList);

                AllDataFromLogFile.Add(TemperatureList);

                AllEventsFromLogFile.Add(EventMessageList);
                AllEventsFromLogFile.Add(EventStatusList);
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

                byte[] AmperageA = { request[27], request[28] };
                byte[] AmperageB = { request[29], request[30] };
                byte[] AmperageC = { request[31], request[32] };

                byte[] Loil = { request[33], request[34] };
                byte[] Toil = { request[35], request[36] };

                byte Poil = request[37];
                byte Temperature = request[38];

                int currentVoltageA = ConvertBytesArrayToInt16Value(VoltageA);
                int currentVoltageB = ConvertBytesArrayToInt16Value(VoltageB);
                int currentVoltageC = ConvertBytesArrayToInt16Value(VoltageC);

                int currentAmperageA = ConvertBytesArrayToInt16Value(AmperageA);
                int currentAmperageB = ConvertBytesArrayToInt16Value(AmperageB);
                int currentAmperageC = ConvertBytesArrayToInt16Value(AmperageC);

                int currentLoil = ConvertBytesArrayToInt16Value(Loil);
                int currentToil = ConvertBytesArrayToInt16Value(Toil);

                int currentPoil = Convert.ToInt32(Poil);
                int currentTemperature = Convert.ToInt32(Temperature);

                //DateOfFirstMessageInRequestList.Add(UnixTimeStampToDateTime(date2));
                //DateOfLastMessageInRequestList.Add(UnixTimeStampToDateTime(date3));

                VoltageAList.Add(currentVoltageA);
                VoltageBList.Add(currentVoltageB);
                VoltageCList.Add(currentVoltageC);

                AmperageAList.Add(currentAmperageA);
                AmperageBList.Add(currentAmperageB);
                AmperageCList.Add(currentAmperageC);

                LoilList.Add(currentLoil);
                ToilList.Add(currentToil);

                PoilList.Add(currentPoil);
                TemperatureList.Add(currentTemperature);
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
            Regex messageInEventData = new Regex(@".{40}");
            Regex statusInEventData = new Regex(@"A|P");

            Match eventDateMatch = dateInEventData.Match(eventData);
            Match statusDataMatch = statusInEventData.Match(eventData);
            Match messageDataMatch = messageInEventData.Match(eventData);

            if (eventDateMatch.Success)
            {
                EventMessageList.Add(Convert.ToString(messageDataMatch));
                EventStatusList.Add(Convert.ToString(statusDataMatch)); 
            }
            else
            {
                //Console.WriteLine("Don't find any values");
            }
        }    


    }
}
