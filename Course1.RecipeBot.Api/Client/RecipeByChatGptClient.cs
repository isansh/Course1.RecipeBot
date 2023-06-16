using Newtonsoft.Json;
using System.Text;

namespace Course1.RecipeBot.Api.Client
{
    public class RecipeByChatGPTClient
    {
        private HttpClient _httpClient;
        private static string? _adress;
        private static string? _apikey;

        public RecipeByChatGPTClient()
        {
            _adress = "https://api.openai.com/";
            _apikey = "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";
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_adress);
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
        }
        public async Task<GptChatCompletionResponse> GetAsyncRecipe(string recipe)
        {
            
            var jsoncontent = "{\r\n     \"model\": \"gpt-3.5-turbo\",\r\n     \"messages\": [{\"role\": \"user\", \"content\": \"" + recipe + "\"}],\r\n     \"temperature\": 0.7\r\n}";
            
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, "v1/chat/completions");
            requestMessage.Content = new StringContent(jsoncontent, Encoding.UTF8, "application/json");
            var responce = await _httpClient.SendAsync(requestMessage);
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<GptChatCompletionResponse>(content);

            return result;
        }
    }
}
