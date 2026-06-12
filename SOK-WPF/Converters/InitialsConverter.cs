using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace SOK_WPF.Converters
{
    class InitialsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text && !string.IsNullOrEmpty(text))
            {
                string[] itab = text.Split(' ');
                string initials = string.Empty;
                for (int i = 0; i < itab.Length && i < 2; i++)
                    initials = initials + itab[i][0];
                return initials;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() ?? "";
        }
    }
}
