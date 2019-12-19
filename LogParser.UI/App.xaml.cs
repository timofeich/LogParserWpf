using LogParser.DataAccess;
using LogParser.UI.DataProvider;
using LogParser.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LogParser.UI
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = new MainWindow(
                new MainViewModel(                
                    new FileDataViewModel(
                        new FileDataProvider(
                            ()=> new FileDataService()))));
            mainWindow.Show();
        }
    }
}
