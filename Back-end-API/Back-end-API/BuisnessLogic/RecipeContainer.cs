using Back_end_API.DAL;
using Back_end_API.DAL.DTO;
using Back_end_API.Interface;

namespace Back_end_API.BuisnessLogic
{
    public class RecipeContainer
    {
        IRecipeContainer recipeContainer;
        RecipeDal recipeDal;

        public RecipeContainer() {; }

        public RecipeContainer(IRecipeContainer dal)
        {
            recipeContainer = dal;
        }

        public async Task AddOneReservation(Recipe recipe)
        {
            RecipeDto dto = recipe.ToDto();
            recipeDal.AddOneRecipe(dto);
        }
    }
}
