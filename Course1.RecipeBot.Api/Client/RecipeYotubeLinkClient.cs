using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using Course1.RecipeBot.Api.Recipe;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Course1.RecipeBot.Api.Client
{
    public class RecipeYotubeLinkClient
    {
        
            private HttpClient _httpClient;
            private static string? _adress;
            private static string? _apikey;

            public RecipeYotubeLinkClient()
            {
                _adress = RecipeConstan.adress;
                _apikey = RecipeConstan.apikey;
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(_adress);
            }

            public async Task<RecipeLinkCompletionResponse> GetAsyncRecipe(string recipe)
            {
                var responce = await _httpClient.GetAsync($"/youtube/v3/search?key=AIzaSyBhgv3lTx3Yj1Ifh-hxY7-IPcgFN5D0TP8&q={recipe}&type=video&maxResults=1");
                responce.EnsureSuccessStatusCode();
                var content = responce.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<RecipeLinkCompletionResponse>(content);
                return result;
            }
        
    }
}
