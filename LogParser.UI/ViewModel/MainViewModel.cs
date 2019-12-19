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