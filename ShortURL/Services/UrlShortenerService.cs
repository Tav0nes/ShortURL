using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Linq.Expressions;
using ShortURL;
using ShortURL.Models;

namespace ShortURL.Services
{
    public class UrlShortenerService
    {
        private readonly HttpClient _httpClient;

        public UrlShortenerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.tinyurl.com/");
        }

        public async Task ShortenerUrlAsync(URL request)
        {
            try
            {
                // Tiny URL API Key
                string apiKey = "wyzyurCGhTeMqvdowmwpp727xolEOcKV0BxpW5IUAXC3CmY8iBs5eGXrnVme";

                // Set the URL of the API
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                // Serialize the content we will post
                var payload = new
                {
                    url = request.OriginalURL
                };

                var json = JsonSerializer.Serialize(payload);
                // Encode it and define its header
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Post to the TinyURL endpoint 'create' and get the response
                using var response = await _httpClient.PostAsync("create", content);

                response.EnsureSuccessStatusCode(); // Throw an exception if the respose status code is not succesfful

                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response to get the tiny_url
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    JsonElement root = doc.RootElement;
                    JsonElement data = root.GetProperty("data");
                    request.ShortenedUrl = data.GetProperty("tiny_url").GetString();
                }
            }

            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}