using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vitual_machine_online_manager.Model;
using System.Windows.Data;

namespace vitual_machine_online_manager.ConvertUI
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not DateTime)
                return "WAIT";

            TimeSpan duration = DateTime.Now.ToUniversalTime().AddHours(7) - (DateTime)value;

            if (duration < Configs.Instance().distanceMaxTime)
                return "OK";

            return "ERROR";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
