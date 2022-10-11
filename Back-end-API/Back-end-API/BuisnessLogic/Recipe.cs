using Back_end_API.DAL.DTO;
using Back_end_API.Interface;

namespace Back_end_API.BuisnessLogic
{
    public class Recipe
    {
        public int recipeId;
        public string Title;
        public string Description;
        public string Ingredients;
        public int PrepTime;
        public int Rating;
        public bool Active;
        public int Portions;
        public int userId;
        public UserDto User;

        IRecipeContainer recipeInterface;

        public Recipe() {; }

        public Recipe(IRecipeContainer dal)
        {
            recipeInterface = dal;
        }

        public Recipe(RecipeDto dto)
        {
            this.recipeId = dto.recipeId;
            this.Title = dto.Title; 
            this.Description = dto.Description;
            this.Ingredients = dto.Ingredients;
            this.PrepTime = dto.PrepTime;
            this.Rating = dto.Rating;
            this.Active = dto.Active;
            this.Portions = dto.Portions;
            this.userId = dto.userId;

            this.User = dto.User;
        }

        public RecipeDto ToDto()
        {
            RecipeDto dto = new RecipeDto();
            dto.recipeId = this.recipeId;
            dto.Title = this.Title;
            dto.Description = this.Description;
            dto.Ingredients = this.Ingredients;
            dto.PrepTime = this.PrepTime;
            dto.Rating = this.Rating;
            dto.Active = this.Active;
            dto.Portions = this.Portions;
            dto.userId = this.userId;

            return dto;
        }
    }
}
