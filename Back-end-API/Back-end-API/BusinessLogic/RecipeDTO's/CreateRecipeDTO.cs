namespace Back_end_API.BusinessLogic
{
    public class CreateRecipeDTO
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Ingredients { get; set; } = null!;

        //[Required]
        //public byte[] Image { get; set; } = null!;

        public int prepTime { get; set; }

        public int Portions { get; set; }

        public int userId { get; set; }
    }
}
