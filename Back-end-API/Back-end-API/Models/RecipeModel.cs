using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.Json.Serialization;

namespace Back_end_API.Models
{
    public class RecipeModel
    {
        [Key]
        public int recipeId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required]
        public string Ingredients { get; set; } = null!;

        [Required]
        public byte[] Image { get; set; } = null!;

        [Required]
        public int? prepTime { get; set; }

        [Required]
        public int? Portions { get; set; }

        public int Rating { get; set; }

        public bool Active { get; set; }

        //Foreign key aanmaken met onderstaande variables.
        [ForeignKey("userId")]
        public int userId { get; set; }

        public UserModel User { get; set; } = null!;
    }
}
