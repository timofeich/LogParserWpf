using LogParser.UI.Events;
using LogParser.UI.ViewModel;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LogParser.Tests.ViewModel
{
    public class FileDataItemViewModelTests
    {
        [Fact]
        public void ShouldPublishOpenFileDataEditViewEvent()
        {
            const int eventDataId = 7;
            const string eventDataMessage = "Cкорость 1 хода включена.";
            const string eventDataStatus = "P";
            DateTime eventDataDateTime = new DateTime(2020, 1, 1, 20, 0, 0);

            var eventMock = new Mock<OpenTableDataEditViewEvent>();
            var eventAggregatorMock = new Mock<IEventAggregator>();
            eventAggregatorMock
                .Setup(ea => ea.GetEvent<OpenTableDataEditViewEvent>())
                .Returns(eventMock.Object);

            var viewModel = new FileDataItemViewModel(eventDataId, eventDataMessage, 
                eventDataDateTime, eventDataStatus, eventAggregatorMock.Object);
            viewModel.OpenFileDataEditViewCommand.Execute(null);

            eventMock.Verify(e => e.Publish(eventDataId) ,Times.Once);
        }


    }
}
