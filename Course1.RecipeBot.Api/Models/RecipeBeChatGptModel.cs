namespace Course1.RecipeBot.Api
{
    public class RecipeBeChatGptModel
    {
        public string? Recipe { get; set; }
        
    }
    public class GptChatCompletionResponse
    {
        public string id { get; set; }
        public string Object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public GptUsage usage { get; set; }
        public GptChoise[] choices { get; set; }
    }
    public class GptChoise
    {
        public GptMessage message { get; set; }
        public string finish_reason { get; set; }
        public int index { get; set; }
    }
    public class GptMessage
    {
        public string role { get; set; }
        public string content { get; set; }
    }
    public class GptUsage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
}
