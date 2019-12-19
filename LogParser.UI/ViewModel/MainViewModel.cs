using LogParser.DataAccess;
using LogParser.UI.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.UI.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(IFileDataViewModel fileDataViewModel)
        {
            FileDataViewModel = fileDataViewModel;
        }

        public IFileDataViewModel FileDataViewModel { get; private set; }

        public void Load()
        {
            FileDataViewModel.Load();
        }
    }
}