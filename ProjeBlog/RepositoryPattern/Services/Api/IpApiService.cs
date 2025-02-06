using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjeBlog.RepositoryPattern.Services.Api
{
    public class IpApiService
    {
        private readonly HttpClient _httpClient;
        public IpApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetCountryFromIpAsync(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress) || ipAddress == "::1")
                return "Localhost"; // IPv6 localhost kontrolü

            try
            {
                string url = $"http://ip-api.com/json/{ipAddress}";
                string response = await _httpClient.GetStringAsync(url);
                dynamic json = JsonConvert.DeserializeObject(response);
                return json?.country ?? "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }
    }
}
