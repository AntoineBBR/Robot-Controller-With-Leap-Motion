using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LegoControler
{
    class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool buttonPressed = (bool)value;
            return buttonPressed ? new SolidColorBrush(Colors.LightSeaGreen) : new SolidColorBrush(Colors.Gray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
