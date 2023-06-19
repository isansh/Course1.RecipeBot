using Course1.RecipeBot.TelegramBot;
namespace Course1.RecipeBot.TelegramBot
{
    internal class Program
    {
        static RecipeTelegramBot recipeBot;

        static void Main(string[] args)
        {
            recipeBot = new RecipeTelegramBot();
            recipeBot.Start();
            Console.ReadKey();
        }
    }
}