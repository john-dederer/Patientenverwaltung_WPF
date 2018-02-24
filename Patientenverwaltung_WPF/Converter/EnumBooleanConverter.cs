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

            if (value != null)
            {
                var parameterValue = Enum.Parse(value.GetType(), parameterString);

                return parameterValue.Equals(value);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var parameterString = parameter as string;
            return parameterString == null ? DependencyProperty.UnsetValue : Enum.Parse(targetType, parameterString);
        }

        #endregion
    }
}