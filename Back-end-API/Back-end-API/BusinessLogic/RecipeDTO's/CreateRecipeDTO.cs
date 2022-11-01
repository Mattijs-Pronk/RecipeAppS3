namespace Back_end_API.BusinessLogic
{
    public class CreateRecipeDTO
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Ingredients { get; set; } = null!;

        public IFormFile imageFile { get; set; } = null!;

        public int prepTime { get; set; }

        public int Portions { get; set; }

        public int userId { get; set; }
    }
}
