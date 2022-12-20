namespace Back_end_API.Models
{
    public class FeaturedRecipes
    {
        public int recipeId { get; set; }
        public RecipeModel Recipe { get; set; } = null!;

        public int featuredId { get; set; }
        public FeaturedModel Featured { get; set; } = null!;
    }
}
