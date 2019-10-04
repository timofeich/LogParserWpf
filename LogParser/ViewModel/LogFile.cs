using Microsoft.Win32;
using System;
using System.IO;

namespace LogParser.ViewModel
{
    public class LogFile
    {
        public string FileName { get; set; }
        public string Length { get; set; }
        public string[] Result { get; set; }

        public LogFile()
        {
            Open();
        }

        private void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Бинарные файлы (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.SafeFileName;
                var content = File.ReadAllLines(openFileDialog.FileName);
                ParseLogFile(content);
            }
        }

        private void ParseLogFile(string[] content)
        {
            Length = Convert.ToString(content.Length);

            foreach (var line in content)
            {
                var spl = line.Split(' ');
                Result = spl;
            }            
        }
    }
}
