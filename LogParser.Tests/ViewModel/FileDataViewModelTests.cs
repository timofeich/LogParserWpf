using LogParser.Model;
using LogParser.UI.DataProvider;
using LogParser.UI.ViewModel;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LogParser.Tests.ViewModel
{
    public class FileDataViewModelTests
    {
        private FileDataViewModel _viewModel;

        public FileDataViewModelTests()
        {
            
        }

        private void SetupFileDataTableDataMockModule()
        {
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var fileDataProviderMock = new Mock<IFileDataProvider>();
            fileDataProviderMock.Setup(dp => dp.GetAllTableData())
                .Returns(new List<TableData>
                {
                    new TableData { Id = 0, AmperageA = 0 },
                    new TableData { Id = 1, AmperageA = 1 },
                });

            _viewModel = new FileDataViewModel(fileDataProviderMock.Object, eventAggregatorMock.Object);
        }

        private void SetupFileDataEventDataMockModule()
        {
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var fileDataProviderMock = new Mock<IFileDataProvider>();
            fileDataProviderMock.Setup(dp => dp.GetAllEventData())
                .Returns(new List<EventData>
                {
                    new EventData { Id = 0, Message = "Cкорость 1 хода включена." },
                    new EventData { Id = 1, Message = "Перегрузка ЭД хода(2 скорость)." },
                });

            _viewModel = new FileDataViewModel(fileDataProviderMock.Object, eventAggregatorMock.Object);
        }

        private void SetupFileDataJoinedEventDataWithTableMockModule()
        {
            var eventAggregatorMock = new Mock<IEventAggregator>();
            var fileDataProviderMock = new Mock<IFileDataProvider>();
            fileDataProviderMock.Setup(dp => dp.GetAllEventJoinedWithTableData())
                .Returns(new List<EventJoinedWithTableData>
                {   
                    new EventJoinedWithTableData{
                        Id = 0,
                        Message = "Cкорость 1 хода включена.",
                        TableDatas = new List<TableData> {
                                new TableData { Id = 0, AmperageA = 0 } } },

                    new EventJoinedWithTableData{
                        Id = 1,
                        Message = "Cкорость 2 хода включена.",
                        TableDatas = new List<TableData> {
                                new TableData { Id = 1, AmperageA = 1 } }},
                });

            _viewModel = new FileDataViewModel(fileDataProviderMock.Object, eventAggregatorMock.Object);
        }

        [Fact]
        public void ShouldLoadTableData()
        {
            SetupFileDataTableDataMockModule();
            _viewModel.Load();

            Assert.Equal(2, _viewModel.TableDatas.Count);

            var tableData = _viewModel.TableDatas.SingleOrDefault(t => t.Id == 0);
            Assert.NotNull(tableData);
            Assert.Equal(0, tableData.AmperageA);

            tableData = _viewModel.TableDatas.SingleOrDefault(t => t.Id == 1);
            Assert.NotNull(tableData);
            Assert.Equal(1, tableData.AmperageA);
        }

        [Fact]
        public void ShouldLoadEventData()
        {
            SetupFileDataEventDataMockModule();

            _viewModel.Load();

            Assert.Equal(2, _viewModel.EventDatas.Count);

            var eventData = _viewModel.EventDatas.SingleOrDefault(t => t.Id == 0);
            Assert.NotNull(eventData);
            Assert.Equal("Cкорость 1 хода включена.", eventData.Message);

            eventData = _viewModel.EventDatas.SingleOrDefault(t => t.Id == 1);
            Assert.NotNull(eventData);
            Assert.Equal("Перегрузка ЭД хода(2 скорость).", eventData.Message);
        }

        [Fact]
        public void ShouldLoadJoinedEventAndTableData()
        {
            SetupFileDataJoinedEventDataWithTableMockModule();

            _viewModel.Load();

            Assert.Equal(2, _viewModel.EventJoinedWithTableDatas.Count);

            var tableDatasTestExample = new List<TableData> {
                                new TableData { Id = 0, AmperageA = 0 } };

            var joinedData = _viewModel.EventJoinedWithTableDatas.SingleOrDefault(t => t.Id == 0);
            Assert.NotNull(joinedData);
            Assert.Equal("Cкорость 1 хода включена.", joinedData.Message);
            Assert.Equal(tableDatasTestExample[0].AmperageA, joinedData.TableDatas[0].AmperageA);

            tableDatasTestExample = new List<TableData> {
                                new TableData { Id = 1, AmperageA = 1 } };

            joinedData = _viewModel.EventJoinedWithTableDatas.SingleOrDefault(t => t.Id == 1);
            Assert.NotNull(joinedData);
            Assert.Equal("Cкорость 2 хода включена.", joinedData.Message);
            Assert.Equal(tableDatasTestExample[0].AmperageA, joinedData.TableDatas[0].AmperageA);
        }

        [Fact]
        public void ShouldLoadTableDataOnlyOnce()
        {
            SetupFileDataTableDataMockModule();

            _viewModel.Load();
            _viewModel.Load();

            Assert.Equal(2, _viewModel.TableDatas.Count);
        }

        [Fact]
        public void ShouldLoadEventDataOnlyOnce()
        {
            SetupFileDataEventDataMockModule();

            _viewModel.Load();
            _viewModel.Load();

            Assert.Equal(2, _viewModel.EventDatas.Count);
        }

        [Fact]
        public void ShouldLoadEventJoinedWithTableDataOnlyOnce()
        {
            SetupFileDataJoinedEventDataWithTableMockModule();

            _viewModel.Load();
            _viewModel.Load();

            Assert.Equal(2, _viewModel.EventJoinedWithTableDatas.Count);
        }
    }

    public class FileDataProviderMock : IFileDataProvider
    {
        public IEnumerable<TableData> GetAllTableData()
        {
            yield return new TableData { Id = 0, AmperageA = 0 };
            yield return new TableData { Id = 1, AmperageA = 1 };
        }

        public IEnumerable<EventData> GetAllEventData()
        {
            yield return new EventData { Id = 0, Message = "Cкорость 1 хода включена."};
            yield return new EventData { Id = 1, Message = "Перегрузка ЭД хода(2 скорость)." };
        }

        public IEnumerable<EventJoinedWithTableData> GetAllEventJoinedWithTableData()
        {
            yield return new EventJoinedWithTableData 
            { 
                Id = 0, 
                Message = "Cкорость 1 хода включена.", 
                TableDatas = new List<TableData> { 
                                new TableData { Id = 0, AmperageA = 0 } } };

            yield return new EventJoinedWithTableData
            {
                Id = 1,
                Message = "Cкорость 2 хода включена.",
                TableDatas = new List<TableData> {
                                new TableData { Id = 1, AmperageA = 1 } }
            };
        }
    }
}



