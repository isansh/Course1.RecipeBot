using Course1.RecipeBot.Api;
using Course1.RecipeBot.Shared;
using Newtonsoft.Json;
using System.Text;

namespace Course1.RecipeBot.TelegramBot
{
    public class RecipeApiClient
    {
        private HttpClient _httpClient;
        private static string? _adress;

        public RecipeApiClient()
        {
            _adress = "https://localhost:7215";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_adress);
        }
        public async Task<RecipeApiModel> GetAsyncRecipe(string request)
        {
            var response = await _httpClient.GetAsync($"/api/Recipe{request}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<RecipeApiModel>(content);
            return result;
        }
    }
}