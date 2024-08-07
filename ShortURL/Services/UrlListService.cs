using System.Net.Http;
using System.Net.Http.Json;
using SharedModels.Models;

namespace ShortURL.Services
{
    public class UrlListService
    {
        private readonly HttpClient _httpClient;

        public UrlListService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("WebApi");

            //_httpClient.BaseAddress = new Uri("https://localhost:8081/");

        }

        public async Task<List<URL>> OnListAsync()
        {
            try
            {
                // Send a GET request to the API endpoint and await the response
                var response = await _httpClient.GetAsync("api/urls");

                // Throw an exception if the response is not successful
                response.EnsureSuccessStatusCode();

                // Read and deserialize the JSON response into a List<URL>
                return await response.Content.ReadFromJsonAsync<List<URL>>() ?? new List<URL>();
            }
            catch (HttpRequestException httpEx)
            {
                // Log HTTP-specific exceptions
                Console.WriteLine($"HTTP error occurred: {httpEx.Message}");
                return new List<URL>();
            }
            catch (Exception ex)
            {
                // Log other exceptions
                Console.WriteLine($"Unexpected error occurred: {ex.Message}");
                return new List<URL>();
            }
        }
    }
}
