using Microsoft.Win32;
using System.IO;

namespace LogParser.ViewModel
{
    public class LogFile
    {
        public string FileName { get; set; }
        public string[] Result;

        public LogFile() { }

        public void Open()
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
            foreach (string line in content)
            {
                Result = line.Split(' ');
            }
        }
    }
}
