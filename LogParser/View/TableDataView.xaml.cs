﻿using LogParser.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

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
            try
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!");
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
            datagrid.SelectAllCells();
            datagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, datagrid);
            datagrid.UnselectAllCells();

            string Clipboardresult = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Файл Excel (*.csv)|*.csv|Excel 2007-2019(*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 3;

            if(saveFileDialog.ShowDialog() == true)
            {
                StreamWriter swObj = new StreamWriter(saveFileDialog.FileName);
                swObj.WriteLine(Clipboardresult);
                swObj.Close();

                Process.Start(saveFileDialog.FileName);
            }
            else
            {
                MessageBox.Show("Ошибка в открытии диалога");
            }
        }
    }
}
