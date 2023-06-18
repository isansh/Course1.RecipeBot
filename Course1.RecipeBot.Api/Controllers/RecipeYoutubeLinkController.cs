using Course1.RecipeBot.Api.Client;
using Course1.RecipeBot.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course1.RecipeBot.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RecipeYoutubeLinkController : ControllerBase
    {
        [HttpGet]
        public RecipeYoutubeLinkModel GetYoutubeLink(string recipeName)
        {
            RecipeYotubeLinkClient recipeClient = new RecipeYotubeLinkClient();
            var resultL = recipeClient.GetAsyncRecipe($"Рецепт {recipeName}");
            var resultLink = resultL.Result;
            if (resultLink.items.Length > 0)
                return new RecipeYoutubeLinkModel
                {
                    Url = "https://www.youtube.com/watch?v=" + resultLink.items[0].id.videoId

                };
            else return
                    new RecipeYoutubeLinkModel
                    {
                        Url = "За вашим запитом нічого не знайдено"

                    }; 
        }
    }
}

