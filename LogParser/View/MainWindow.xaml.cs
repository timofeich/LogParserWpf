using LogParser.ViewModel;
using System;
using System.Windows;

namespace LogParser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private void MenuItem_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
