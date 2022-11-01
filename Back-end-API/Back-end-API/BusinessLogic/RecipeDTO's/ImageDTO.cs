using Microsoft.AspNetCore.Http;

namespace Back_end_API.BusinessLogic.RecipeDTO_s
{
    public class ImageDTO
    {
        public IFormFile imageFile { get; set; } = null!;
    }
}
