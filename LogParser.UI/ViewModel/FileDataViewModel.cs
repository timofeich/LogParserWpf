using LogParser.DataAccess;
using LogParser.Model;
using LogParser.UI.DataProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
