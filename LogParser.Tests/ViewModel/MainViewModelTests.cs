using System;
using System.Collections.Generic;
using System.Linq;
using LogParser.Model;
using LogParser.UI.Events;
using LogParser.UI.ViewModel;
using Moq;
using Prism.Events;
using Xunit;
using LogParser.Tests.Extensions;

namespace LogParser.Tests.ViewModel
{
    public class MainViewModelTests
    {
        private Mock<IFileDataViewModel> _fileDataViewModelMock;
        private OpenTableDataEditViewEvent _openTableDataEditViewEvent;
        private Mock<IEventAggregator> _eventAggregatorMock;
        private MainViewModel _viewModel;
        private List<Mock<IFileDataEditViewModel>> _fileDataEditViewModelMocks;

        public MainViewModelTests()
        {
            _fileDataEditViewModelMocks = new List<Mock<IFileDataEditViewModel>>();
            _fileDataViewModelMock = new Mock<IFileDataViewModel>();

            _openTableDataEditViewEvent = new OpenTableDataEditViewEvent();
            _eventAggregatorMock = new Mock<IEventAggregator>();
            _eventAggregatorMock.Setup(ea => ea.GetEvent<OpenTableDataEditViewEvent>())
                .Returns(_openTableDataEditViewEvent);

            _viewModel = new MainViewModel(_fileDataViewModelMock.Object, CreateFileDataEditViewModel, 
                _eventAggregatorMock.Object);
        }

        private IFileDataEditViewModel CreateFileDataEditViewModel()
        {
            var fileDataEditViewModelMock = new Mock<IFileDataEditViewModel>();
            fileDataEditViewModelMock.Setup(vm => vm.Load(It.IsAny<int>()))
                .Callback<int>(eventDataId =>
                {
                    fileDataEditViewModelMock.Setup(vm => vm.eventData)
                    .Returns(new EventData { Id = eventDataId });
                });
            _fileDataEditViewModelMocks.Add(fileDataEditViewModelMock);
            return fileDataEditViewModelMock.Object;
        }

        [Fact]
        public void ShouldCallTheLoadMethodOfTheFileDataViewModel()
        {
            _viewModel.Load();

            _fileDataViewModelMock.Verify(vm => vm.Load(), Times.Once);
        }
        
        [Fact]
        public void ShouldAddTableDataEditViewModelAndLoadAndSelectIt()
        {
            const int eventDataId = 7;
            _openTableDataEditViewEvent.Publish(eventDataId);

            Assert.Equal(1, _viewModel.FileDataEditViewModels.Count);
            var eventDataEditVm = _viewModel.FileDataEditViewModels.First();
            Assert.Equal(eventDataEditVm, _viewModel.SelectedFileDataViewModel);
            _fileDataEditViewModelMocks.First().Verify(vm => vm.Load(eventDataId), Times.Once);

        }

        [Fact]
        public void ShouldAddFileDataEditViewModelsOnlyOnce()
        {
            _openTableDataEditViewEvent.Publish(5);
            _openTableDataEditViewEvent.Publish(5);
            _openTableDataEditViewEvent.Publish(6);
            _openTableDataEditViewEvent.Publish(7);
            _openTableDataEditViewEvent.Publish(7);

            Assert.Equal(3, _viewModel.FileDataEditViewModels.Count);
        }

        [Fact]
        public void ShouldRaisePropertyChangedEventForSelectedFileDataEditViewModel()
        {
            var fileDataEditVmMock = new Mock<IFileDataEditViewModel>();
            var deleted = _viewModel.IsPropertyChangedDeleted(()=> 
            {
                _viewModel.SelectedFileDataViewModel = fileDataEditVmMock.Object;
            }, nameof(_viewModel.SelectedFileDataViewModel));


            Assert.True(deleted);
        }
    }
}
