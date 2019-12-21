﻿using LogParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogParser.DataParse
{
    public class FileDataParsing
    {
        public string FileName { get; }

        public List<TableData> listTable = new List<TableData>();
        public List<EventData> listEvent = new List<EventData>();

        private int tableNotesCounter = 0;
        public int eventNotesCounter = 0;
        private DateTime DateOfMessageInRequest { get; set; }

        private EventParsing eventParsing;
        private DataParsing dataParsing;


        public FileDataParsing(string FileName)
        {
            this.FileName = FileName;

            eventParsing = new EventParsing();
            dataParsing = new DataParsing();

            ParseLogFile();
        }

        private void ParseLogFile()
        {
            ReadFileByRequests(FileName);
        }

        public void ReadFileByRequests(string fileName)
        {
            List<byte[]> listOfRequests = new List<byte[]>();

            using (BinaryReader b = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
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
            byte[] dateOfFirstMessage = { request[8], request[9], request[10], request[11] };
            byte[] dateOfLastMessage = { request[12], request[13], request[14], request[15] };

            byte eventIdentifier = 65;

            int date2 = BitConverter.ToInt32(dateOfFirstMessage, 0);
            int date3 = BitConverter.ToInt32(dateOfLastMessage, 0);

            List<DateTime> TableDataDate = SetDateOfMessageSending(UnixTimeStampToDateTime(date2), UnixTimeStampToDateTime(date3));

            int CountOfMessagesInRequest = 0;

            for (int i = 19; i < request.Length;)
            {
                if (request[i] == 88 && request[i + 1] == 49)
                {
                    if (TableDataDate.Count > CountOfMessagesInRequest)
                    {
                        DateOfMessageInRequest = TableDataDate[CountOfMessagesInRequest];
                        CountOfMessagesInRequest++;
                    }
                    else
                    {
                        DateOfMessageInRequest = TableDataDate[CountOfMessagesInRequest - 1].AddTicks(100);
                        CountOfMessagesInRequest++;
                    }

                    dataParsing.GetData();
                    TableData tbl = new TableData();
                    GetTableDataFromLogFile(tbl, request, i);
                    listTable.Add(tbl);
                    i += 23;
                }
                else if (request[i - 2] == eventIdentifier)
                {
                    string infoAboutEvent = Encoding.Default.GetString(request, i, 64);

                    EventData emp = new EventData();
                    GetEventDataFromLogFile(emp, infoAboutEvent);
                    listEvent.Add(emp);
                    i += 67;
                }
                else
                {
                    i = request.Length;
                }
            }
        }

        private List<DateTime> SetDateOfMessageSending(DateTime dateOfFirstMessage, DateTime dateOfLastMessage)
        {
            List<DateTime> MessageSendingDateList = new List<DateTime>();

            int frameDateCount = 0;
            int maxFramesDateInRequest = 23;

            while (dateOfFirstMessage <= dateOfLastMessage)
            {
                frameDateCount++;

                MessageSendingDateList.Add(dateOfFirstMessage);
                dateOfFirstMessage = dateOfFirstMessage.AddSeconds(1);

                if (frameDateCount > maxFramesDateInRequest)
                    break;
            }

            return MessageSendingDateList;
        }

        public DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1980, 1, 1, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp);
            return dtDateTime;
        }

        public void GetTableDataFromLogFile(TableData e, byte[] request, int i)
        {
            byte[] VoltageA = { request[i + 2], request[i + 3] };
            byte[] VoltageB = { request[i + 4], request[i + 5] };
            byte[] VoltageC = { request[i + 6], request[i + 7] };

            byte[] AmperageA = { request[i + 8], request[i + 9] };
            byte[] AmperageB = { request[i + 10], request[i + 11] };
            byte[] AmperageC = { request[i + 12], request[i + 13] };

            byte[] Loil = { request[i + 14], request[i + 15] };
            byte[] Toil = { request[i + 16], request[i + 17] };

            byte Poil = request[i + 18];
            byte Temperature = request[i + 19];

            e.Id = tableNotesCounter;

            e.Date = DateOfMessageInRequest;

            e.VoltageA = BitConverter.ToInt16(VoltageA, 0);
            e.VoltageB = BitConverter.ToInt16(VoltageB, 0);
            e.VoltageC = BitConverter.ToInt16(VoltageC, 0);

            e.AmperageA = BitConverter.ToInt16(AmperageA, 0);
            e.AmperageB = BitConverter.ToInt16(AmperageB, 0);
            e.AmperageC = BitConverter.ToInt16(AmperageC, 0);

            e.Loil = BitConverter.ToInt16(Loil, 0);
            e.Toil = BitConverter.ToInt16(Toil, 0);

            e.Poil = Convert.ToInt32(Poil);
            e.ThyristorTemperature = Convert.ToInt32(Temperature);

            tableNotesCounter++;
        }
        public void GetEventDataFromLogFile(EventData e, string eventData)
        {
            Regex dateInEventData = new Regex(@"\d\d:\d\d:\d\d (\d\d)/(\d\d)/(\d\d)");
            Regex messageInEventData = new Regex(@".{40}");
            Regex statusInEventData = new Regex(@"A|P");

            Match eventDateMatch = dateInEventData.Match(eventData);
            Match statusDataMatch = statusInEventData.Match(eventData);
            Match messageDataMatch = messageInEventData.Match(eventData);

            if (eventDateMatch.Success)
            {
                string dateTime = Convert.ToString(eventDateMatch);
                eventNotesCounter++;

                e.Id = eventNotesCounter;
                e.Message = Convert.ToString(messageDataMatch);
                e.Date = DateTime.Parse(dateTime);

                if (Convert.ToString(statusDataMatch) == "")
                {
                    e.Status = "C";
                }
                else
                {
                    e.Status = Convert.ToString(statusDataMatch);
                }
            }
        }
    }
}
