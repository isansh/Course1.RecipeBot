using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course1.RecipeBot.TelegramBot
{
    public class RecipeApiModel
    {
        public string Recipe { get; set; }
        public int ChatId { get; set; }
        public string DateAdded { get; set; }
        public string Url { get; set; }
        public int Count { get; set; }
    }
    public class AddToFavoriteModel
    {
        public string recipe { get; set; }
        public long chatId { get; set; }
        public string dateAdded { get; set; }
    }
    public class RemoveFromFavoritesModel
    {
        public string recipe { get; set; }
        public long chatId { get; set; }
    }
}

