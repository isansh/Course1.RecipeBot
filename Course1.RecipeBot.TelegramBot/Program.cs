using Course1.RecipeBot.TelegramBot;
namespace Course1.RecipeBot.TelegramBot
{
    internal class Program
    {
        static void Main(string[] args)
        {

            RecipeTelegramBot recipe22Bot = new RecipeTelegramBot();
            recipe22Bot.Start();
            Console.ReadKey();
        }
    }
}