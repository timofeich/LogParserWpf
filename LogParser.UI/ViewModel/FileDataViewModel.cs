using LogParser.Model;
using LogParser.UI.DataProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LogParser.UI.ViewModel
{
    public interface IFileDataViewModel
    {
        void Load();
    }

    public class FileDataViewModel : ViewModelBase, IFileDataViewModel
    {
        private IFileDataProvider _dataProvider;
        private FileInformation _fileInformation;

        public FileDataViewModel(IFileDataProvider dataProvider)
        {
            TableDatas = new ObservableCollection<TableData>();
            EventDatas = new ObservableCollection<EventData>();
            EventJoinedWithTableDatas = new ObservableCollection<EventJoinedWithTableData>();

            FileInformation = new FileInformation();

            _dataProvider = dataProvider;
        }
        public void Load()
        {
            TableDatas.Clear();
            foreach(var tableData in _dataProvider.GetAllTableData())
            {
                TableDatas.Add(tableData);
            }

            EventDatas.Clear();
            foreach (var eventData in _dataProvider.GetAllEventData())
            {
                EventDatas.Add(eventData);
            }

            EventJoinedWithTableDatas.Clear();
            foreach (var eventJoinedWithTableData in _dataProvider.GetAllEventJoinedWithTableData())
            {
                EventJoinedWithTableDatas.Add(eventJoinedWithTableData);
            }

            _fileInformation = _dataProvider.GetAllInformationAboutFile();
            FileInformation = _fileInformation;
        }

        public ObservableCollection<TableData> TableDatas { get; private set; }
        public ObservableCollection<EventData> EventDatas { get; private set; }
        public ObservableCollection<EventJoinedWithTableData> EventJoinedWithTableDatas { get; private set; }

        public FileInformation FileInformation { get; private set; }
    }
}
