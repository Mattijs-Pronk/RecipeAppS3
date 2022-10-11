using Back_end_API.DAL.DTO;
using Back_end_API.Interface;
using DatabaseLayer;

namespace Back_end_API.DAL
{
    public class RecipeDal : SqlConnect//, IRecipeContainer
    {
        public async Task AddOneRecipe(RecipeDto dto)
        {
            OpenConnect();

            cmd.Parameters.Clear();

            cmd.CommandText = "INSERT INTO Recipes (title, description, ingredients, preptime, rating, active, portions, userid) " +
            "VALUES (@title, @description, @ingredients, @preptime, @rating, @active, @portions, @userid)";

            cmd.Parameters.AddWithValue("@title", dto.Title);
            cmd.Parameters.AddWithValue("@description", dto.Description);
            cmd.Parameters.AddWithValue("@ingredients", dto.Ingredients);
            cmd.Parameters.AddWithValue("@preptime", dto.PrepTime);
            cmd.Parameters.AddWithValue("@rating", dto.Rating);
            cmd.Parameters.AddWithValue("@active", dto.Active);
            cmd.Parameters.AddWithValue("@portions", dto.Portions);
            cmd.Parameters.AddWithValue("@userid", dto.userId);

            cmd.ExecuteNonQuery();

            CloseConnect();
        }
    }
}
