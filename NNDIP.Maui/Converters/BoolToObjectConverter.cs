using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.Maui.Converters
{
    internal class BoolToObjectConverter<T> : IValueConverter
    {
        public T TrueObject { get; set; }
        public T FalseObject { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TrueObject : FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((T)value).Equals(TrueObject);
        }
    }
}
