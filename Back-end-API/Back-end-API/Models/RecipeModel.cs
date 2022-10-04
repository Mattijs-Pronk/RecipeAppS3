using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;

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
        public string prepTime { get; set; } = null!;

        [Required]
        public string Portions { get; set; } = null!;

        public int Rating { get; set; }

        public bool Active { get; set; }

        public int? userId { get; set; }

        [ForeignKey("userId")]
        public virtual UserModel User { get; set; } = null!;
    }
}
