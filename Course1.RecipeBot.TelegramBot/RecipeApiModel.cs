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
        public string ChatId { get; set; }
        public string DateAdded { get; set; }
        public string Url { get; set; }
        public int Count { get; set; }
    }
}

