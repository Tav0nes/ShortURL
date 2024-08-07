using System.Net.Http.Json;
using SharedModels.Models;

namespace ShortURL.Services
{
    public class UrlListService
    {
        private readonly HttpClient _httpClient;

        public UrlListService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:8081/");

        }

        public async Task<List<URL>> OnListAsync()
        {
            // Call the API endpoint to get the list of URLs
            return await _httpClient.GetFromJsonAsync<List<URL>>("api/urls");
        }
    }
}
