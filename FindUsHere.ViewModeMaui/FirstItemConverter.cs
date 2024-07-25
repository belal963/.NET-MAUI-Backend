using System;
using System.Collections;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace FindUsHere.Converters
{
    public class FirstItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IList list && list.Count > 0)
            {
                return list[0];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
