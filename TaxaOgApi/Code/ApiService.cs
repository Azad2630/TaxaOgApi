using System;
using System.Net.Http;
using System.Threading.Tasks;
using TaxaOgApi;

public class ApiService
{
    public readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDistance(string addressStart, string addressEnd)
    {
        
            string apiKey = "AIzaSyAi9pU1OmMqpvh4vYZ8udkJZ4ZyKh1aBiY";
            string apiUrl = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={Uri.EscapeDataString(addressStart)}&destinations={Uri.EscapeDataString(addressEnd)}&key={apiKey}";

            var response = await _httpClient.GetStringAsync(apiUrl);
            Console.WriteLine($"API Response: {response}");
            return response;
        
    }
}
