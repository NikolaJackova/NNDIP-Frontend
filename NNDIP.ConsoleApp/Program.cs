// See https://aka.ms/new-console-template for more information

using NNDIP.ApiClient;
using System.Net.Http;

string uri = "https://localhost:7093/";
var client = new ApiClient(uri, new HttpClient());
TokenDto tokenDto = await client.LoginAsync(new LoginDto()
{
    Username="admin",
    Password= "$2a$10$ZEghMxwsCOz7sdqwgtrPM.BSdUzBWc5BAwmV6fT2vyuO5AGIyb04q"
});
Console.WriteLine(tokenDto.Token);

var http = new HttpClient();
http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenDto.Token);
client = new ApiClient(uri, http);
ICollection<SensorDto> sensorDtos = await client.SensorGetAsync();
foreach (var sensor in sensorDtos)
{
    Console.WriteLine(sensor.Name);
}
Console.ReadKey();

