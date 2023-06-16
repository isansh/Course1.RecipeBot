using Course1.RecipeBot.Api;
using Course1.RecipeBot.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Course1.RecipeBot.Api.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class FavoriteRecipeController : Controller
    {
        string connectionString = "Server=art1023-test-db-server.database.windows.net;Database=RecipeDB;User Id=rodaragoi;Password=Zaqwes123@;";
        [HttpPost]
        public IActionResult AddToFavorites(string recipe, long chatId, DateTime dateCallBackQuery)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO FavoriteRecipes (Recipe, Id, DateCallBackQuery) VALUES (@Recipe, @Id, @DateCallBackQuery)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Recipe", recipe);
                    command.Parameters.AddWithValue("@Id", chatId);
                    command.Parameters.AddWithValue("@DateCallBackQuery", dateCallBackQuery);

                    int rowsAffected = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteFromFavorites(string recipe, long chatId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM FavoriteRecipes WHERE Recipe = @Recipe AND Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Recipe", recipe);
                    command.Parameters.AddWithValue("@Id", chatId);


                    int rowsAffected = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return Ok();
        }

        [HttpGet]
        public FavoriteRecipeModel GetFavoriteRecipes(int numberOfRecipe, long chatId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = $"SELECT Recipe FROM FavoriteRecipes WHERE Id = @chatid ORDER BY DateCallBackQuery OFFSET {numberOfRecipe} ROWS FETCH NEXT 1 ROWS ONLY";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    object recipedb;
                    command.Parameters.AddWithValue("@chatid", chatId);
                    object result = command.ExecuteScalar();
                    
                        if (result != null)
                    {
                        string recipe = result.ToString();
                        return new FavoriteRecipeModel
                        {
                            Recipe = recipe
                        };
                    }
                    else return new FavoriteRecipeModel
                    {
                        Recipe = "Рецепт не знайдено"
                    };
                    connection.Close();
                }
                
            }
        }

        [HttpGet]
        public FavoriteRecipeModel GetCountFavoriteRecipes(long chatId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT COUNT(*) FROM FavoriteRecipes WHERE Id = @chatId";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@chatId", chatId);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return new FavoriteRecipeModel
                    {
                        Count = count
                    };
                    connection.Close();
                }
            }
        }
    }
}
