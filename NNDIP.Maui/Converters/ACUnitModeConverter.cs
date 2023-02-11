using NNDIP.Maui.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.Maui.Converters
{
    internal class ACUnitModeConverter : IValueConverter
    {
        public string Auto { get; set; } = Icons.DeviceStatusAutoLight;
        public string Cool { get; set; } = Icons.DeviceStatusColdLight;
        public string Dry { get; set; } = Icons.DeviceStatusDryLight;
        public string Heat { get; set; } = Icons.DeviceStatusHeatWaveLight;
        public string Fan { get; set; } = Icons.DeviceStatusFanLight;
        public string None { get; set; } = "";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value switch
            {
                "AUTO" => Auto,
                "COOL" => Cool,
                "DRY" => Dry,
                "HEAT" => Heat,
                "FAN" => Fan,
                _ => None,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string mode = (string)value;
            if (mode.Equals(Auto))
            {
                return "AUTO";
            }
            else if (mode.Equals(Cool))
            {
                return "COOL";
            }
            else if (mode.Equals(Dry))
            {
                return "DRY";
            }
            else if (mode.Equals(Heat))
            {
                return "HEAT";
            }
            else if (mode.Equals(Fan))
            {
                return "FAN";
            }
            else
            {
                return "None";
            }
        }

    }
}
