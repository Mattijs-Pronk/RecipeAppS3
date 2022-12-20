using System.ComponentModel.DataAnnotations;

namespace Back_end_API.Models
{
    public class FeaturedModel
    {
        [Key]
        public int featuredId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public IList<FeaturedRecipes> FeaturedRecipes { get; set; } = null!;
    }
}
