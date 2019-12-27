using LogParser.UI.Events;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LogParser.UI.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private IFileDataEditViewModel _selectedFileDataViewModel;
        public IFileDataViewModel FileDataViewModel { get; private set; }

        private Func<IFileDataEditViewModel> _fileDataEditVmCreator;

        public MainViewModel(IFileDataViewModel fileDataViewModel, Func<IFileDataEditViewModel> fileDataEditVmCreator,
            IEventAggregator eventAggregator)
        {
            FileDataViewModel = fileDataViewModel;
            FileDataEditViewModels = new ObservableCollection<IFileDataEditViewModel>();
            _fileDataEditVmCreator = fileDataEditVmCreator;
            eventAggregator.GetEvent<OpenTableDataEditViewEvent>().Subscribe(OnOpenFileDataEditView);
        }

        private void OnOpenFileDataEditView(int eventDataId)
        {
            var fileDataEditVm = FileDataEditViewModels.SingleOrDefault(vm => vm.eventData.Id == eventDataId);
            if (fileDataEditVm == null)
            {
                fileDataEditVm = _fileDataEditVmCreator();
                FileDataEditViewModels.Add(fileDataEditVm);
                fileDataEditVm.Load(eventDataId);
            }
            SelectedFileDataViewModel = fileDataEditVm;
        }

        public ObservableCollection<IFileDataEditViewModel> FileDataEditViewModels { get; private set; }
        
        public IFileDataEditViewModel SelectedFileDataViewModel 
        {
            get
            { 
                return _selectedFileDataViewModel; 
            }
            set
            {
                _selectedFileDataViewModel = value;
                OnPropertyChanged();
            }
        }
        
        public void Load()
        {
            FileDataViewModel.Load();
        }
    }
}