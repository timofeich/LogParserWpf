using LogParser.DataAccess;
using LogParser.UI.Command;
using Microsoft.Win32;
using System.Windows.Input;

namespace LogParser.UI.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IFileDataViewModel fileDataViewModel)
        {
            FileDataViewModel = fileDataViewModel;
            ChooseLogFile = new DelegateCommand(arg => OpenLogFile());
        }

        private void OpenLogFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Бинарные файлы (*.bin)|*.bin|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;

            if (openFileDialog.ShowDialog() == true)
            {
                FileDataService fileDataService = new FileDataService();
                fileDataService.StorageFile = openFileDialog.SafeFileName;

                //FileName = openFileDialog.SafeFileName;

                //ParseLogFile(openFileDialog.FileName);
            }
        }

        public IFileDataViewModel FileDataViewModel { get; private set; }
        public ICommand ChooseLogFile { get; private set; }
        public void Load()
        {
            FileDataViewModel.Load();
        }
    }
}