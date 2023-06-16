namespace Course1.RecipeBot.Api
{
    public class RecipeYoutubeLinkModel
    {
        public string Url { get; set; }
    }
    public class RecipeLinkCompletionResponse
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string nextPageToken { get; set; }
        public string regionCode { get; set; }
        public RecipePageInfo pageInfo { get; set; }
        public RecipeItems[] items { get; set; }
    }
    public class RecipeItems
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public RecipeId id { get; set; }
    }
    public class RecipeId
    {
        public string kind { get; set; }
        public string videoId { get; set; }
    }
    public class RecipePageInfo
    {
        public int totalResults { get; set; }
        public int resultsPerPage { get; set; }
    }
}
