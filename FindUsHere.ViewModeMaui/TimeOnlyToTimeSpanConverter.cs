using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindUsHere.ViewModeMaui
{
    public class TimeOnlyToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeOnly timeOnly)
            {
                return timeOnly.ToTimeSpan();
            }
            return TimeSpan.Zero;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return TimeOnly.FromTimeSpan(timeSpan);
            }
            return TimeOnly.MinValue;
        }
    }

    public class TimeOnlyToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeOnly timeOnly)
            {
                return timeOnly.ToString("HH:mm");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string timeString && TimeOnly.TryParseExact(timeString, "HH:mm", out TimeOnly timeOnly))
            {
                return timeOnly;
            }
            return TimeOnly.MinValue;
        }
    }
}
