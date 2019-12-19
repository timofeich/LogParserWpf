using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            //FileDataViewModel = new FileDataViewModel();
        }

        public FileDataViewModel FileDataViewModel { get; private set; }

        public void Load()
        {
            FileDataViewModel.Load();
        }
    }
}