using Course1.RecipeBot.Api;
using Course1.RecipeBot.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using Course1.RecipeBot.Api.Client;

namespace Course1.RecipeBot.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        [HttpGet]
        public RecipeBeChatGptModel GetRecipeMealKind(MealKind mealKind)
        {
            RecipeByChatGPTClient gptclient = new RecipeByChatGPTClient();
            var resultTask = gptclient.GetAsyncRecipe($"Рецепт на {mealKind}, укр, назва, інгредієнти, спосіб");
            //if (resultTask.Id != 200)
            //    return new RecipeBeChatGptModel
            //    {
            //        Recipe = "Нічого не знайдено"
            //    };
            //else
            {
                var result = resultTask.Result;
                if (result.choices.Length > 0)
                    return new RecipeBeChatGptModel
                    {
                        Recipe = result.choices[0].message.content
                    };
                else return new RecipeBeChatGptModel
                {
                    Recipe = "Нічого не знайдено"
                };
            }
        }

        [HttpGet]
        public RecipeBeChatGptModel GetRecipeByIngridients(string ingridients)
        {
            RecipeByChatGPTClient gptclient = new RecipeByChatGPTClient();
            var resultTask = gptclient.GetAsyncRecipe($"Рецепт з {ingridients}, укр, назва, інгридієнти, спосіб");
            //if (resultTask.Id != 200)
            //    return new RecipeBeChatGptModel
            //    {
            //        Recipe = "Нічого не знайдено"
            //    };
            //else
            {
                var result = resultTask.Result;
                if (result.choices.Length > 0)
                    return new RecipeBeChatGptModel
                    {
                        Recipe = result.choices[0].message.content
                    };
                else return new RecipeBeChatGptModel
                {
                    Recipe = "Нічого не знайдено"
                };
            }
        }
    }
    
}
