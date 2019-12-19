using LogParser.DataAccess;
using LogParser.UI.DataProvider;
using LogParser.UI.ViewModel;
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
