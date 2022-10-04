using System.ComponentModel.DataAnnotations;

namespace Back_end_API.Models
{
    public class UserModel
    {
        [Key]
        public int userId { get; set; }

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public bool isAdmin { get; set; }
    }
}
