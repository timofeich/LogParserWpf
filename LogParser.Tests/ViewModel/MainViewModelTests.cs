using LogParser.UI.ViewModel;
using Moq;
using Xunit;

namespace LogParser.Tests.ViewModel
{
    public class MainViewModelTests
    {
        private Mock<IFileDataViewModel> _fileDataViewModelMock;
        private MainViewModel _viewModel;

        public MainViewModelTests()
        {
            _fileDataViewModelMock = new Mock<IFileDataViewModel>();
            _viewModel = new MainViewModel(_fileDataViewModelMock.Object);
        }

        [Fact]
        public void ShouldCallTheLoadMethodOfTheFileDataViewModel()
        {
            _viewModel.Load();

            _fileDataViewModelMock.Verify(vm => vm.Load(), Times.Once);
        }
    }
}
