namespace Back_end_API.DAL.DTO
{
    public class RecipeDto
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
    }
}
