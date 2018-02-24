using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Patientenverwaltung_WPF.Converter
{
    public class EnumBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is string parameterString))
                return DependencyProperty.UnsetValue;

            if (value != null && Enum.IsDefined(value.GetType(), value) == false)
                return DependencyProperty.UnsetValue;

            if (value == null) return null;
            var parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(parameter is string parameterString) ? DependencyProperty.UnsetValue : Enum.Parse(targetType, parameterString);
        }

        #endregion
    }
}