using Course1.RecipeBot.Shared;
using Newtonsoft.Json;
using System.Text;
using Telegram.Bot.Types;

namespace Course1.RecipeBot.TelegramBot
{
    public class RecipeApiClient
    {
        private HttpClient _httpClient;
        private static string? _adress;

        public RecipeApiClient()
        {
            _adress = "https://course1recipebotapi.azurewebsites.net"; // https://localhost:7215
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
        public async Task<AddToFavoriteModel> AddToFavoritesAsync(string recipe, long chatId, string date)
        {
            var model = new AddToFavoriteModel
            {
                recipe = recipe,
                chatId = chatId,
                dateAdded = date
            };

            var jsonContent = JsonConvert.SerializeObject(model);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/RecipeFavorites/AddToFavorites"); //?recipe={recipe}%chatId={chatId}&DateAdded={date}
            requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var responce = await _httpClient.SendAsync(requestMessage);
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<AddToFavoriteModel>(content);
            return result;
        }
        public async Task<RecipeApiModel> DeleteAsyncRecipe(string recipe, long chatId)
        {
            var model = new RemoveFromFavoritesModel
            {
                recipe = recipe,
                chatId = chatId
            };
            var jsonContent = JsonConvert.SerializeObject(model);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Delete, "/api/RecipeFavorites/DeleteFromFavorites"); //?recipe={recipe}%chatId={chatId}&DateAdded={date}
            requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var responce = await _httpClient.SendAsync(requestMessage);
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<RecipeApiModel>(content);
            return result;
        }
    }
}