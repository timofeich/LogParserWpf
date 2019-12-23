using LogParser.Model;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LogParser.UI.View
{
    /// <summary>
    /// Логика взаимодействия для FileDataView.xaml
    /// </summary>
    public partial class FileDataView : UserControl
    {
        private EventJoinedWithTableData ResultOfSearshingInJoinedTable { get; set; }
        private DateTime DateOfEvent { get; set; }

        public FileDataView()
        {
            InitializeComponent();
        }

        private void SetFontSizeClick(object sender, RoutedEventArgs e)
        {
            double newSize;

            if (double.TryParse(TableDataTextBox.Text, out newSize))
            {
                FontSize = newSize;
            }
        }

        private void SelectJoinedEventDateByTableDate(object sender, MouseButtonEventArgs e)
        {
            SearchValueInJoinedData(DataGridWithTableData, 1);
        }

        private void SelectJoinedEventDateByEventDate(object sender, MouseButtonEventArgs e)
        {
            SearchValueInJoinedData(DataGridWithEventData, 2);
        }

        private void SearchValueInJoinedData(DataGrid tableDataView, int dateColumn)
        {
            try
            {
                var item = tableDataView.SelectedValue;
                var content = (tableDataView.SelectedCells[dateColumn].Column.GetCellContent(item) as TextBox).Text;

                DateOfEvent = Convert.ToDateTime(content);

                ResultOfSearshingInJoinedTable = JoinedEventData.Items.Cast<EventJoinedWithTableData>().FirstOrDefault(w => w.Date == DateOfEvent);

                if (ResultOfSearshingInJoinedTable == null)
                {
                    SearchValueInJoinedDataWithInterval(0, 120);
                }
                else
                {
                    FocusOnSearchResult();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
            }
        }

        private void SearchValueInJoinedDataWithInterval(int fromNumber, int toNumber)
        {
            for (int i = fromNumber; i < toNumber; i++)
            {
                ResultOfSearshingInJoinedTable = JoinedEventData.Items.Cast<EventJoinedWithTableData>().FirstOrDefault(w => w.Date == DateOfEvent.AddSeconds(i));
                if (ResultOfSearshingInJoinedTable != null)
                {
                    FocusOnSearchResult();
                    break;
                }
            }
        }

        private void FocusOnSearchResult()
        {
            TabItem3.IsSelected = true;

            JoinedEventData.UpdateLayout();
            JoinedEventData.ScrollIntoView(JoinedEventData.Items[ResultOfSearshingInJoinedTable.Id - 1]);
            JoinedEventData.Focus();

            JoinedEventData.SelectedItem = JoinedEventData.Items[ResultOfSearshingInJoinedTable.Id - 1];
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel(DataGridWithEventData);
        }

        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel(DataGridWithTableData);
        }

        private void ExportToExcel(DataGrid datagrid)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файл Excel (*.csv)|*.csv|Excel 2007-2019(*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 3;

            if (saveFileDialog.ShowDialog() == true)
            {
                datagrid.SelectAllCells();
                datagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, datagrid);
                datagrid.UnselectAllCells();

                string Clipboardresult = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);

                StreamWriter swObj = new StreamWriter(saveFileDialog.FileName);
                swObj.WriteLine(Clipboardresult);
                swObj.Close();

                MessageBox.Show("Файл успешно сохранен.", "Сообщение", MessageBoxButton.OK);
            }
        }
    }
}
