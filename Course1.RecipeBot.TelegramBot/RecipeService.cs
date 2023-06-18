using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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
                return result.Recipe;
        }

        public string GetRecipeByIngredients(string ingridients)
        {
            ingridients = $"/GetRecipeByIngridients?ingridients={ingridients}";
            RecipeApiClient recipeApiClient = new RecipeApiClient();
            var resultTask = recipeApiClient.GetAsyncRecipe(ingridients);
            var result = resultTask.Result;
                return result.Recipe;
        }

        public string GetReceipeVideoLink(string recipeName)
        {
            recipeName = HttpUtility.UrlEncode(recipeName);
            recipeName = recipeName.ToUpper();
            recipeName = $"YoutubeLink/GetYoutubeLink?recipeName={recipeName}";
            RecipeApiClient recipeClient = new RecipeApiClient();
            var resultL = recipeClient.GetAsyncRecipe(recipeName);
            var resultLink = resultL.Result;
                return resultLink.Url;
        }

        public string AddToFavorites(string recipe, long chatId, DateTime dateCallBackQuery)
        {
            string dateAdded = dateCallBackQuery.ToString("yyyy-MM-ddTHH:mm:ss");
            RecipeApiClient recipeApiClient = new RecipeApiClient();
            var resultTask = recipeApiClient.AddToFavoritesAsync(recipe, chatId, dateAdded);
            var result = resultTask.Result;
            return "Додано";
        }

        public string DeleteFromFavorites(string recipe, long chatId)
        {
            recipe = $"Favorites/DeleteFromFavorites?recipe={recipe}&chatId={chatId}";
            RecipeApiClient recipeApiClient = new RecipeApiClient();
            var resultTask = recipeApiClient.DeleteAsyncRecipe(recipe);
            var result = resultTask.Result;
            return "Видалено";
        }

        public string GetFavoriteRecipes(int numberRecipe, long chatId)
        {
            string recipe = $"Favorites/GetFavoriteRecipes?numberOfRecipe={numberRecipe}&chatId={chatId}";
            RecipeApiClient recipeApiClient = new RecipeApiClient();
            var resultTask = recipeApiClient.GetAsyncRecipe(recipe);
            return resultTask.Result.Recipe;
        }
        public int CountFavoriteRecipe(long chatId)
        {
            string recipe = $"Favorites/GetCountFavoriteRecipes?chatId={chatId}";
            RecipeApiClient recipeApiClient = new RecipeApiClient();
            var resultTask = recipeApiClient.GetAsyncRecipe(recipe);
            return resultTask.Result.Count;
        }
    }
}
