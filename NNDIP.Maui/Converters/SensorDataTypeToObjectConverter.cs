﻿using NNDIP.Maui.Models;
using System.Globalization;

namespace NNDIP.Maui.Converters
{
    public class SensorDataTypeToObjectConverter : IValueConverter
    {
        public string Temperature { get; set; } = Icons.SensorStatusTemperatureDark;
        public string Humidity { get; set; } = Icons.SensorStatusHumidityDark;
        public string Co2 { get; set; } = Icons.SensorStatusCo2Dark;
        public string None { get; set; } = Icons.SensorStatusUnknownTypeDark;

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
