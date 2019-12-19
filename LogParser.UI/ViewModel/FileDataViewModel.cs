using LogParser.Model;
using LogParser.UI.DataProvider;
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
        public FileDataViewModel(IFileDataProvider dataProvider)
        {
            TableDatas = new ObservableCollection<TableData>();
            _dataProvider = dataProvider;
        }
        public void Load()
        {
            TableDatas.Clear();
            foreach(var tableData in _dataProvider.GetAllTableData())
            {
                TableDatas.Add(tableData);
            }
        }

        public ObservableCollection<TableData> TableDatas { get; private set; }
    }
}
