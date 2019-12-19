using LogParser.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LogParser.Tests.ViewModel
{
    public class MainViewModelTests
    {
        [Fact]
        public void ShouldCallTheLoadMethodOfTheFileDataViewModel()
        {
            var fileDataViewModelMock = new FileDataViewModelMock();
            var viewModel = new MainViewModel(fileDataViewModelMock);

            viewModel.Load();

            Assert.True(fileDataViewModelMock.LoadHasBeenCalled);

        }
    }

    public class FileDataViewModelMock: IFileDataViewModel
    {
        public bool LoadHasBeenCalled { get; set; }
        public void Load()
        {
            LoadHasBeenCalled = true;
        }
    }
}
