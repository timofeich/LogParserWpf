using LogParser.Model;
using LogParser.ViewModel;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LogParser.View
{
    /// <summary>
    /// Логика взаимодействия для TableDataView.xaml
    /// </summary>
    public partial class TableDataView : Window
    {
        private EventJoinedWithTableData result { get; set; }

        private DateTime dateOfEvent { get; set; }
        public TableDataView()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double newSize;

            if (double.TryParse(txtFontSize.Text, out newSize))
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
            var item = tableDataView.SelectedValue;
            var content = (tableDataView.SelectedCells[dateColumn].Column.GetCellContent(item) as TextBox).Text;

            dateOfEvent = Convert.ToDateTime(content);

            result = JoinedEventData.Items.Cast<EventJoinedWithTableData>().FirstOrDefault(w => w.Date == dateOfEvent);

            if (result == null)
            {
                SearchValueInJoinedDataWithInterval(0, 120);
            }
            else
            {
                FocusOnSearchResult();
            }
        }

        private void SearchValueInJoinedDataWithInterval(int leftInterval, int rightInterval)
        {
            for (int i = leftInterval; i < rightInterval; i++)
            {
                result = JoinedEventData.Items.Cast<EventJoinedWithTableData>().FirstOrDefault(w => w.Date == dateOfEvent.AddSeconds(i));
                if (result != null)
                {
                    FocusOnSearchResult();
                    break;
                }
            }
        }

        private void FocusOnSearchResult()
        {
            TabItem3.IsSelected = true;

            JoinedEventData.ScrollIntoView(JoinedEventData.Items[result.ID - 1]);

            JoinedEventData.SelectedItem = JoinedEventData.Items[result.ID - 1];
        }

    }
}
