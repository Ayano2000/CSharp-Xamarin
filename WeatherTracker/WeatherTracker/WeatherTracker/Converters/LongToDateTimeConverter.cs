using System;
using System.Globalization;
using Xamarin.Forms;

namespace WeatherTracker.Converters
{
    public class LongToDateTimeConverter : IValueConverter
    {
        DateTime _time = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine("VALUE " + value);
            Console.WriteLine("TARGET TYPE " + targetType);
            Console.WriteLine("PARAMETER " + parameter);
            Console.WriteLine("CULTURE " + culture);
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