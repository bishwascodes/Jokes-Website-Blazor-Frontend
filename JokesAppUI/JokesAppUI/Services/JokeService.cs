using JokesAppUI.Models;
using System.Text;
using System.Text.Json;
namespace JokesAppUI.Services;


public class JokeService
{
    private readonly HttpClient _http;

    public JokeService(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient("JokesAPI");
    }

    // ==============================
    // GET all jokes
    // ==============================
    public async Task<List<Joke>> GetJokesAsync()
    {
        try
        {
            // API returns text/plain, but it's JSON text — handle it manually
            var response = await _http.GetAsync("api/Jokes");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return new List<Joke>();
            }

            var json = await response.Content.ReadAsStringAsync();
            var jokes = System.Text.Json.JsonSerializer.Deserialize<List<Joke>>(json,
                new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return jokes ?? new List<Joke>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching jokes: {ex.Message}");
            return new List<Joke>();
        }
    }

    // ==============================
    // POST a new joke
    // ==============================
    public async Task<Joke?> AddJokeAsync(NewJokeRequest newJoke)
    {
        try
        {
            var json = JsonSerializer.Serialize(newJoke);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("api/Jokes", content);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error adding joke: {response.StatusCode}");
                return null;
            }

            var responseJson = await response.Content.ReadAsStringAsync();
            var createdJoke = JsonSerializer.Deserialize<Joke>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return createdJoke;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception adding joke: {ex.Message}");
            return null;
        }
    }

}
