using Autofac;
using LogParser.DataAccess;
using LogParser.UI.DataProvider;
using LogParser.UI.Startup;
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
            var bootStrapper = new BootStrapper();
            var container = bootStrapper.BootStrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
