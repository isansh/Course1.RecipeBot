namespace Course1.RecipeBot.Api.Models
{
    public class AddToFavoriteModel
    {
        public string recipe { get; set; }
        public long chatId { get; set; }
        public string dateAdded { get; set; }
    }
    public class RemoveFromFavoriteModel
    {
        public string recipe { get; set; }
        public long chatId { get; set; }
    }
}
