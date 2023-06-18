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
            _adress = "https://course1recipebotapi.azurewebsites.net"; //    https://localhost:7215
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
        public async Task<RecipeApiModel> AddToFavoritesAsync(string recipe, long chatId, string date)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"/api/RecipeFavorites/AddToFavorites?recipe={recipe}&chatId={chatId}&DateAdded={date}");//
            var responce = await _httpClient.SendAsync(requestMessage);
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<RecipeApiModel>(content);
            return result;
        }
        public async Task<RecipeApiModel> DeleteAsyncRecipe(string request)
        {
            var response = await _httpClient.DeleteAsync($"/api/Recipe{request}");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<RecipeApiModel>(content);
            return result;
        }
    }
}