using LogParser.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LogParser.View
{
    /// <summary>
    /// Логика взаимодействия для TableDataView.xaml
    /// </summary>
    public partial class TableDataView : Window
    {
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
    }
}
