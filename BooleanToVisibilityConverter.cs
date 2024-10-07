using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Mader_Control_System
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed; // Comportamento padrão
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility visibilityValue && visibilityValue == Visibility.Visible;
        }
    }
}