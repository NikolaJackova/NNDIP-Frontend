using NNDIP.Maui.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.Maui.Converters
{
    internal class FanSpeedToIconConverter : IValueConverter
    {
        public string Speed0 { get; set; } = Icons.DeviceStatusSignalEmptyDark;
        public string Speed1 { get; set; } = Icons.DeviceStatusSignal1Dark;
        public string Speed2 { get; set; } = Icons.DeviceStatusSignal2Dark;
        public string Speed3 { get; set; } = Icons.DeviceStatusSignal3Dark;
        public string Speed4 { get; set; } = Icons.DeviceStatusSignal4Dark;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value switch
            {
                "Quiet" => Speed1,
                "Low" => Speed2,
                "Medium" => Speed3,
                "High" => Speed4,
                _ => Speed0,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string speed = (string)value;
            if (speed.Equals(Speed1))
            {
                return "Quiet";
            }
            else if (speed.Equals(Speed2))
            {
                return "Low";
            }
            else if (speed.Equals(Speed3))
            {
                return "Medium";
            }
            else if (speed.Equals(Speed4))
            {
                return "High";
            }
            else
            {
                return "None";
            }
        }
    }
}
