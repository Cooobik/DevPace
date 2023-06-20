using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DevPace.Desktop.Helpers.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isValid && !isValid)
            {
                return Colors.Tomato;
            }

            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
