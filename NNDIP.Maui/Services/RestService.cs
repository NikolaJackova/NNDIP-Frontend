using NNDIP.ApiClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNDIP.Maui.Services
{
    public static class RestService
    {
        //TODO uri put somewhere else
        //public static string URI { get; set; } = "http://10.0.2.2:8081";
        //public static string URI { get; set; } = "http://192.168.5.30:8081";
        public static string URI { get; set; } = "http://195.181.223.203:8083";
        
        private static IApiClient _API = CreateApiClient();
        public static IApiClient API { get => _API; }

        private static IApiClient CreateApiClient()
        {
            HttpClient http = new();
            string result = Task.Run(AuthenticationService.GetJwtToken).Result;
            if (result is not null)
            {
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result);
            }
            return new ApiClient.ApiClient(URI, http);
        }
        public static void SetAuthorization(string token)
        {
            HttpClient http = new();
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            _API = new ApiClient.ApiClient(URI, http);
        }
        public static void ClearAuthorization()
        {
            _API = new ApiClient.ApiClient(URI, new HttpClient());
        }
    }
}
