using Autofac;
using LogParser.DataAccess;
using LogParser.UI.DataProvider;
using LogParser.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.UI.Startup
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<FileDataViewModel>().As<IFileDataViewModel>();

            builder.RegisterType<FileDataProvider>().As<IFileDataProvider>();

            builder.RegisterType<FileDataService>().As<IDataService>();

            return builder.Build();
        }
    }
}
