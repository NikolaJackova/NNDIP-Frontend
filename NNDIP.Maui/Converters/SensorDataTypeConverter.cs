using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.Maui.Converters
{
    public class SensorDataTypeConverter : IValueConverter
    {
        public string Temperature { get; set; } = "temp.png";
        public string Humidity { get; set; } = "humidity.png";
        public string Co2 { get; set; } = "co2.png";
        public string None { get; set; } = "unknown_type.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value switch
            {
                "TEMPERATURE" => Temperature,
                "HUMIDITY" => Humidity,
                "CO2" => Co2,
                _ => None,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string speed = (string)value;
            if (speed.Equals(Temperature))
            {
                return "TEMPERATURE";
            }
            else if (speed.Equals(Humidity))
            {
                return "HUMIDITY";
            }
            else if (speed.Equals(Co2))
            {
                return "CO2";
            }
            else
            {
                return "NONE";
            }
        }

    }
}
