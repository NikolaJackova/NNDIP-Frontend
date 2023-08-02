using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.ApiClient
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.18.2.0 (NJsonSchema v10.8.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AddressStateResult
    {
        [Newtonsoft.Json.JsonProperty("isACUnitOn", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool IsACUnitOn { get; set; }

        [Newtonsoft.Json.JsonProperty("isFanOn", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool IsFanOn { get; set; }

        [Newtonsoft.Json.JsonProperty("acTemperature", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AcTemperature { get; set; }

        [Newtonsoft.Json.JsonProperty("acUnitMode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AcUnitMode { get; set; }

        [Newtonsoft.Json.JsonProperty("acUnitFanSpeed", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AcUnitFanSpeed { get; set; }

        [Newtonsoft.Json.JsonProperty("isRecuperationOn", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool IsRecuperationOn { get; set; }

    }
}
