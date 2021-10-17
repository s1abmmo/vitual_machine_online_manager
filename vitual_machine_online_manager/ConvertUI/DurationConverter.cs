using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace vitual_machine_online_manager.ConvertUI
{
    public class DurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is not DateTime)
                return "---";

            TimeSpan duration = DateTime.Now.ToUniversalTime().AddHours(7) - (DateTime)value;
            return duration.ToString(@"dd\.hh\:mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
