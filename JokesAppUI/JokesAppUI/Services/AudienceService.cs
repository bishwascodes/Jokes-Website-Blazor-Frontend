using JokesAppUI.Models;
using System.Text;
using System.Text.Json;

namespace JokesAppUI.Services
{
    public class AudienceService
    {
        private readonly HttpClient _http;

        public AudienceService(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("JokesAPI");
        }

        // ==============================
        // GET all audiences
        // ==============================
        public async Task<List<Audience>> GetAudiencesAsync()
        {
            try
            {
                var response = await _http.GetAsync("api/Audiences");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error fetching audiences: {response.StatusCode}");
                    return new List<Audience>();
                }

                var json = await response.Content.ReadAsStringAsync();
                var audiences = JsonSerializer.Deserialize<List<Audience>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return audiences ?? new List<Audience>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception fetching audiences: {ex.Message}");
                return new List<Audience>();
            }
        }

        // ==============================
        // POST a new audience
        // ==============================
        public async Task<Audience?> AddAudienceAsync(NewAudienceRequest newAudience)
        {
            try
            {
                var json = JsonSerializer.Serialize(newAudience);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/Audiences", content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error adding audience: {response.StatusCode}");
                    return null;
                }

                var responseJson = await response.Content.ReadAsStringAsync();
                var createdAudience = JsonSerializer.Deserialize<Audience>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return createdAudience;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception adding audience: {ex.Message}");
                return null;
            }
        }
    }
}
