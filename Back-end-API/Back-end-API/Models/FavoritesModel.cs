using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Models
{
    public class FavoritesModel
    {
        [Key]
        public int favoriteId { get; set; }

        //Foreign key aanmaken met onderstaande variables.
        [ForeignKey("userId")]
        public int userId { get; set; }

        //Foreign key aanmaken met onderstaande variables.
        [ForeignKey("recipeId")]
        public int recipeId { get; set; }

        public RecipeModel Recipe { get; set; } = null!;
    }
}
