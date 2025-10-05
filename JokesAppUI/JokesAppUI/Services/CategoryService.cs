using System.Net.Http.Json;
using JokesAppUI.Models;
using System.Text;
using System.Text.Json;

namespace JokesAppUI.Services
{
    public class CategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient("JokesAPI");
        }

        // ==============================
        // GET all categories
        // ==============================
        public async Task<List<Category>> GetCategoriesAsync()
        {
            try
            {
                var response = await _http.GetAsync("api/Categories");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error fetching categories: {response.StatusCode}");
                    return new List<Category>();
                }

                var json = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<Category>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return categories ?? new List<Category>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception fetching categories: {ex.Message}");
                return new List<Category>();
            }
        }

        // ==============================
        // POST a new category
        // ==============================
        public async Task<Category?> AddCategoryAsync(NewCategoryRequest newCategory)
        {
            try
            {
                var json = JsonSerializer.Serialize(newCategory);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _http.PostAsync("api/Categories", content);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error adding category: {response.StatusCode}");
                    return null;
                }

                var responseJson = await response.Content.ReadAsStringAsync();
                var createdCategory = JsonSerializer.Deserialize<Category>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return createdCategory;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception adding category: {ex.Message}");
                return null;
            }
        }
    }
}
