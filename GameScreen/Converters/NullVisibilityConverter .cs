using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace GameScreen.Converters
{
    public class NullVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var invisibilityWhenNull = parameter is Visibility paramVisibility ? 
                paramVisibility : 
                parameter is string paramString &&
                Enum.TryParse<Visibility>(paramString, true, out var stringVisibility) ? 
                    stringVisibility : 
                    Visibility.Collapsed;
            return value == null ? invisibilityWhenNull : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}