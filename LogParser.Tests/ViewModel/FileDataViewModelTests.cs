﻿using LogParser.Model;
using LogParser.UI.DataProvider;
using LogParser.UI.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LogParser.Tests.ViewModel
{
    public class FileDataViewModelTests
    {
        [Fact]
        public void ShouldLoadTableData()
        {
            var viewModel = new FileDataViewModel(new FileDataProviderMock());

            viewModel.Load();

            Assert.Equal(2, viewModel.TableDatas.Count);

            var tableData = viewModel.TableDatas.SingleOrDefault(t => t.Id == 0);
            Assert.NotNull(tableData);
            Assert.Equal(0, tableData.AmperageA);

            tableData = viewModel.TableDatas.SingleOrDefault(t => t.Id == 1);
            Assert.NotNull(tableData);
            Assert.Equal(1, tableData.AmperageA);
        }

        [Fact]
        public void ShouldLoadEventData()
        {
            var viewModel = new FileDataViewModel(new FileDataProviderMock());

            viewModel.Load();

            Assert.Equal(2, viewModel.EventDatas.Count);

            var tableData = viewModel.EventDatas.SingleOrDefault(t => t.Id == 0);
            Assert.NotNull(tableData);
            Assert.Equal("Cкорость 1 хода включена.", tableData.Message);

            tableData = viewModel.EventDatas.SingleOrDefault(t => t.Id == 1);
            Assert.NotNull(tableData);
            Assert.Equal("Cкорость 2 хода включена.", tableData.Message);
        }

        [Fact]
        public void ShouldLoadTableDataOnlyOnce()
        {
            var viewModel = new FileDataViewModel(new FileDataProviderMock());

            viewModel.Load();
            viewModel.Load();

            Assert.Equal(2, viewModel.TableDatas.Count);
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
            yield return new EventData { Id = 0, Message = "Cкорость 1 хода включена." };
            yield return new EventData { Id = 1, Message = "Cкорость 2 хода включена." };
        }
    }
}



