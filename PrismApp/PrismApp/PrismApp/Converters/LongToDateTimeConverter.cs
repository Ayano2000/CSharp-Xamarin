using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace PrismApp.Converters
{
    class LongToDateTimeConverter : IValueConverter
    {
        DateTime _time = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is long longDate)
            {
                return $"{_time.AddSeconds(longDate).ToString()} GMT";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{_time.ToString()} GMT";
            //throw new NotImplementedException();
        }
    }
}
