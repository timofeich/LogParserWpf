using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser.Tests.Extensions
{
    public static class NotifyPropertyChangedExtensions
    {
        public static bool IsPropertyChangedDeleted(
            this INotifyPropertyChanged notifyPropertyChanged,
            Action action, string propertyName)
        {
            var deleted = false;
            notifyPropertyChanged.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    deleted = true;
                }
            };

            action();

            return deleted;
        }

    }
}
