using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.ApiClient
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class LimitPlanSettings
    {
        [Newtonsoft.Json.JsonProperty("yearPeriodDto", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public SimpleYearPeriodDto YearPeriodDto { get; set; }

        [Newtonsoft.Json.JsonProperty("optimalValueTemperature", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double OptimalValueTemperature { get; set; }

        [Newtonsoft.Json.JsonProperty("optimalValueCo2", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public double OptimalValueCo2 { get; set; }

        [Newtonsoft.Json.JsonProperty("temperatureLow", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Threshold TemperatureLow { get; set; }

        [Newtonsoft.Json.JsonProperty("temperatureHigh", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Threshold TemperatureHigh { get; set; }

        [Newtonsoft.Json.JsonProperty("co2", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Threshold Co2 { get; set; }

        [Newtonsoft.Json.JsonProperty("isWinterActive", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool IsWinterActive { get; set; }

        [Newtonsoft.Json.JsonProperty("isSummerActive", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool IsSummerActive { get; set; }

    }
}
