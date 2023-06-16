using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Course1.RecipeBot.Api;
using Course1.RecipeBot.Api.Client;
using Course1.RecipeBot.Api.Controllers;
using Course1.RecipeBot.Shared;


namespace Course1.RecipeBot.TelegramBot
{
    internal class RecipeService
    {

        public string GetRecipe(MealKind mealKind)
        {
            string recipeMealKind = $"/GetRecipeMealKind?mealKind={mealKind}";
            RecipeApiClient recipeApiClient = new RecipeApiClient();
            var resultTask = recipeApiClient.GetAsyncRecipe(recipeMealKind);
            var result = resultTask.Result;
            //if (result.choices.Length > 0)
                return result.recipe;
            //else return "За вашим запитом нічого не знайдено";
        }

        public string GetRecipeByIngredients(string ingridients)
        {
            ingridients = $"/GetRecipeByIngridients?ingridients={ingridients}";
            RecipeApiClient recipeApiClient = new RecipeApiClient();
            var resultTask = recipeApiClient.GetAsyncRecipe(ingridients);
            var result = resultTask.Result;
            //if (result.choices.Length > 0)
                return result.recipe;
            //else return "За вашим запитом нічого не знайдено";
        }

        public string GetReceipeVideoLink(string recipeName)
        {
            recipeName = HttpUtility.UrlEncode(recipeName);
            recipeName = recipeName.ToUpper();
            recipeName = $"YoutubeLink?recipeName={recipeName}";
            RecipeApiClient recipeClient = new RecipeApiClient();
            var resultL = recipeClient.GetAsyncRecipe(recipeName);
            var resultLink = resultL.Result;
            //if (resultLink.items.Length > 0)
                return resultLink.url;
            //else return "За вашим запитом нічого не знайдено";
        }

        public string AddToFavorites(string recipe, long chatId, DateTime dateCallBackQuery)
        {
            FavoriteRecipeController favoriteRecipeController = new FavoriteRecipeController();
            favoriteRecipeController.AddToFavorites(recipe, chatId, dateCallBackQuery);
            return "Додано";
        }

        public string DeleteFromFavorites(string recipe, long chatId)
        {
            FavoriteRecipeController favoriteRecipeController = new FavoriteRecipeController();
            favoriteRecipeController.DeleteFromFavorites(recipe, chatId);
            return "Видалено";
        }
        public string GetFavoriteRecipes(int numberRecipe, long chatId)
        {
            FavoriteRecipeController favoriteRecipeController = new FavoriteRecipeController();
            var recipe = favoriteRecipeController.GetFavoriteRecipes(numberRecipe, chatId);
            return recipe.Recipe;
        }
        public int CountFavoriteRecipe(long chatId)
        {
            FavoriteRecipeController favoriteRecipeController = new FavoriteRecipeController();
            int result = favoriteRecipeController.GetCountFavoriteRecipes(chatId).Count;
            return result;
        }
    }
}
