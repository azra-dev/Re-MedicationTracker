using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MedicationTracker.Core
{
    public class TimeSpanToTimeStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                int hours = timeSpan.Hours;
                string period = hours >= 12 ? "PM" : "AM";
                hours = (hours > 12 ? hours - 12 : (hours == 0 ? hours + 12 : hours));
                return $"{hours}:{timeSpan.Minutes.ToString("00")} {period}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
