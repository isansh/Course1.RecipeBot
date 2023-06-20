using Course1.RecipeBot.Shared;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
//using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Course1.RecipeBot.TelegramBot
{
    internal class RecipeTelegramBot
    {
        DateTime dateCallBackQuery;
        long chatId;
        string action;
        int helperYoutube;
        int numberrecipe;
        string youtuberecipe;
        string previousmessagewithingridients;
        string previousmessage;
        string More;
        TelegramBotClient botClient = new TelegramBotClient("5951590671:AAEvvOAzMjXlGT22WlqO1-tibb2NIREY0Ek");
        CancellationToken token = new CancellationToken();
        ReceiverOptions receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

        RecipeService recipeService = new RecipeService();

        public async Task Start()
        {
            botClient.StartReceiving(HandletUpdateAsync, HandletErrorAsync, receiverOptions, token);
            var botMe = await botClient.GetMeAsync();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"Бот {botMe.Username} почав працювати");
            Console.ReadKey();
        }

        private Task HandletErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationtoken)
        {
            var ErrorMesage = exception switch
            {
                ApiRequestException apiRequestException => $"Помилка в телеграм бот АПІ:\n {apiRequestException.ErrorCode}" +
                $"\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMesage);
            return Task.CompletedTask;
        }

        private async Task HandletUpdateAsync(ITelegramBotClient botclient, Update update, CancellationToken cancellationtoken)
        {
            try
            {
                if (update.Type == UpdateType.CallbackQuery)
                {
                    dateCallBackQuery = update.CallbackQuery.Message.Date;
                    chatId = update.CallbackQuery.Message.Chat.Id;
                    if (update.CallbackQuery.Data == "add")
                    {
                        action = this.recipeService.AddToFavorites(update.CallbackQuery.Message.Text, chatId, dateCallBackQuery);
                        var markup = new InlineKeyboardMarkup(
                        new[]
                            {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Видалити з обраного", "remove")
                        }
                            }
                    );


                        await botClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id,
                              update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: markup);
                        numberrecipe++;
                        return;
                    }
                    else if (update.CallbackQuery.Data == "remove")
                    {
                        action = recipeService.DeleteFromFavorites(update.CallbackQuery.Message.Text, chatId);
                        var markup = new InlineKeyboardMarkup(new[]
                        {
                        InlineKeyboardButton.WithCallbackData("♥ Додати в обране", "add"),

                    });
                        await botClient.EditMessageTextAsync(update.CallbackQuery.Message.Chat.Id,
                             update.CallbackQuery.Message.MessageId, update.CallbackQuery.Message.Text, replyMarkup: markup);
                        numberrecipe--;
                        return;
                    }

                }
                else
                    if (update.Message == null)
                    return;
                else
                    // Only process text messages
                    chatId = update.Message.Chat.Id;
                if (update.Type == UpdateType.Message && update?.Message?.Text != null)
                {
                    await HandleMessageAsync(botClient, update.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Сталась помилка. Будь ласка, зверніться до адміністратора");
            }

        }
        private async Task HandleMessageAsync(ITelegramBotClient botClient, Message message)
        {
            string recipe;

            if (message.Text == "Назад")
            {
                message.Text = "/start";
                numberrecipe = 0;
                helperYoutube = 0;
            }
            if (message.Text == "Ще один рецепт")
            {
                message.Text = "Сніданок";
            }
            if (message.Text == "Ще варіант")
            {
                message.Text = "Обід";
            }
            if (message.Text == "Наступний рецепт")
            {
                message.Text = "Вечеря";
            }
            if (message.Text == "/favorite")
            {
                numberrecipe = 0;
            }
            if (message.Text == "Наступний")
            {
                numberrecipe++;
                message.Text = "/favorite";
            }

            if (message.Text == "/start")
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                    (
                      new[]
                      {
                          new KeyboardButton[] {"Сніданок", "Обід"},
                          new KeyboardButton[] {"Вечеря", "З моїх інгредієнтів"},
                          //new KeyboardButton[] {"Знайти рецепт в ютубі за назвою"}
                      }
                    )
                {
                    ResizeKeyboard = true
                };
                //ReplyKeyboardMarkup replyKeyboardMarkup=Markups();
                await botClient.SendTextMessageAsync(message.Chat.Id, "Обери потрібну кнопку нижче або в Меню.", replyMarkup: replyKeyboardMarkup);
                return;
            }
            if (message.Text == "Сніданок")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Будь ласка, зачекайте 20 секунд. Бот шукає рецепт");

                recipe = recipeService.GetRecipe(MealKind.Breakfast);
                More = "Ще один рецепт";
                GetKeyboards(More, message, recipe);

                return;
            }
            if (message.Text == "Обід")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Будь ласка, зачекайте 20 секунд. Бот шукає рецепт");
                recipe = recipeService.GetRecipe(MealKind.Lunch);
                More = "Ще варіант";
                GetKeyboards(More, message, recipe);

                return;
            }
            if (message.Text == "Вечеря")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Будь ласка, зачекайте 20 секунд. Бот шукає рецепт");
                recipe = recipeService.GetRecipe(MealKind.Dinner);
                More = "Наступний рецепт";
                GetKeyboards(More, message, recipe);

                return;
            }

            if (message.Text == "Ще один")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Будь ласка, зачекайте 20 секунд. Бот шукає рецепт");
                helperYoutube = 0;
                recipe = recipeService.GetRecipeByIngredients(previousmessagewithingridients);
                More = "Ще один";
                GetKeyboards(More, message, recipe);

                return;
            }
            if (message.Text == "Ще")
            {

                helperYoutube++;
                recipe = recipeService.GetReceipeVideoLink(youtuberecipe + helperYoutube);
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                        (
                          new[]
                          {
                          new KeyboardButton[] {"Ще", "Назад"},
                          }
                        )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, recipe, replyMarkup: replyKeyboardMarkup);

                return;
            }

            if (message.Text == "/favorite")
            {
                int count = recipeService.CountFavoriteRecipe(chatId);
                helperYoutube = 0;
                if (count == 0)
                {
                    ReplyKeyboardMarkup replyKeyboardMarkup = new
                       (
                         new[]
                         {
                          new KeyboardButton[] {"", "Назад"},
                         }
                       )
                    {
                        ResizeKeyboard = true
                    };
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Збережених рецептів ще немає", replyMarkup: replyKeyboardMarkup);
                    return;
                }
                else
                if (count == numberrecipe + 1 && message.Text != "/youtube")
                {
                    recipe = recipeService.GetFavoriteRecipes(numberrecipe, chatId);
                    More = "";
                    GetKeyboardFavorite(More, message, recipe);
                    numberrecipe = 0;
                    return;
                }

                else
                {

                    recipe = recipeService.GetFavoriteRecipes(numberrecipe, chatId);
                    if (recipe == "Рецепт не знайдено")
                    {
                        ReplyKeyboardMarkup replyKeyboardMarkup = new
                       (
                         new[]
                         {
                          new KeyboardButton[] {"", "Назад"},
                         }
                       )
                        {
                            ResizeKeyboard = true
                        };
                        await botClient.SendTextMessageAsync(message.Chat.Id, recipe, replyMarkup: replyKeyboardMarkup);
                        return;
                    }
                    else
                    {
                        More = "Наступний";
                        GetKeyboardFavorite(More, message, recipe);
                        return;
                    }
                }
            }



            if (message.Text == "З моїх інгредієнтів")
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new
                        (
                          new[]
                          {
                          new KeyboardButton[] {"", "Назад"},
                          }
                        )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть ваші інгредієнти через кому", replyMarkup: replyKeyboardMarkup);
                previousmessage = "Введіть ваші інгредієнти через кому";
                return;
            }
            else
        if (message.Text == "/youtube")
            {
                helperYoutube++;
                numberrecipe = 0;
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                        (
                          new[]
                          {
                          new KeyboardButton[] {"", "Назад"},
                          }
                        )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введіть назву страви", replyMarkup: replyKeyboardMarkup);
                previousmessage = "Введіть назву страви";
                return;
            }
            else if (previousmessage == "Введіть назву страви")
            {
                recipe = recipeService.GetReceipeVideoLink(message.Text + helperYoutube);
                if (recipe == "За вашим запитом нічого не знайдено")
                {
                    More = "";
                }
                else More = "Ще";
                ReplyKeyboardMarkup replyKeyboardMarkup = new
                        (
                          new[]
                          {
                          new KeyboardButton[] {More, "Назад"},
                          }
                        )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, recipe, replyMarkup: replyKeyboardMarkup);
                previousmessage = "";
                youtuberecipe = message.Text;
            }
            else
        if (previousmessage == "Введіть ваші інгредієнти через кому")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Будь ласка, зачекайте 20 секунд. Бот шукає рецепт");
                recipe = recipeService.GetRecipeByIngredients(message.Text);
                More = "Ще один";
                GetKeyboards(More, message, recipe);
                previousmessage = "";
                previousmessagewithingridients = message.Text;
            }
            else
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new
                        (
                          new[]
                          {
                          new KeyboardButton[] {"", "Назад"},
                          }
                        )
                {
                    ResizeKeyboard = true
                };
                await botClient.SendTextMessageAsync(message.Chat.Id, "Ви помилились, введіть, будь ласка дані ще раз", replyMarkup: replyKeyboardMarkup);
            }
        }


        public async void GetKeyboards(string More, Message message, string recipe)
        {
            InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(
                new[]
                {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("♥ Додати в обране", "add")
            }
                }
            );

            ReplyKeyboardMarkup replyKeyboardMarkup = new ReplyKeyboardMarkup(
                new[]
                {
            new KeyboardButton[] { More, "Назад"},

                }
            )
            {
                ResizeKeyboard = true
            };
            await botClient.SendTextMessageAsync(message.Chat.Id, "Знайдений рецепт:", replyMarkup: replyKeyboardMarkup);
            await botClient.SendTextMessageAsync(message.Chat.Id, recipe, replyMarkup: inlineKeyboardMarkup);


        }

        public async void GetKeyboardFavorite(string More, Message message, string recipe)
        {
            string text = "Видалити з обраного";

            InlineKeyboardMarkup inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                InlineKeyboardButton.WithCallbackData(text, "remove"),
                }
            });

            ReplyKeyboardMarkup replyKeyboardMarkup = new
                (
                  new[]
                  {
                          new KeyboardButton[] {More, "Назад"},

                  }
                )
            {
                ResizeKeyboard = true
            };
            await botClient.SendTextMessageAsync(message.Chat.Id, "Збережений рецепт:", replyMarkup: replyKeyboardMarkup);
            await botClient.SendTextMessageAsync(message.Chat.Id, recipe, replyMarkup: inlineKeyboard);
        }
    }
}

