using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public string imageName { get; set; } = null!;

        [Required]
        public int? prepTime { get; set; }

        [Required]
        public int? Portions { get; set; }

        public int Rating { get; set; }

        public string Status { get; set; } = null!;

        //Foreign key aanmaken met onderstaande variables.
        [ForeignKey("userId")]
        public int userId { get; set; }

        public UserModel User { get; set; } = null!;

        public enum status
        {
            Accepted,
            Onhold,
            Declined
        }
    }
}
