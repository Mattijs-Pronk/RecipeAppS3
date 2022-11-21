namespace Back_end_API.BusinessLogic.RecipeDTO_s
{
    public class EditRecipeDTO
    {
        public int recipeId { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Ingredients { get; set; } = null!;

        public int prepTime { get; set; }

        public int Portions { get; set; }
    }
}
