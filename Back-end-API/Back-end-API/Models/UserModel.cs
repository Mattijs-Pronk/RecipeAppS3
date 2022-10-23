using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Models
{
    public class UserModel
    {
        [Key]
        public int userId { get; set; }

        [Required]
        public string userName { get; set; } = string.Empty;

        [Required]
        public byte[] passwordHash { get; set; } = null!;

        [Required]
        public byte[] passwordSalt { get; set; } = null!;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public bool isAdmin { get; set; }


        //account activeren
        public string? activateAccountToken { get; set; }

        public DateTime? activateAccountTokenExpires { get; set; }


        //wachtwoord resetten
        public string? passwordResetToken { get; set; }

        public DateTime? passwordResetTokenExpires { get; set; }



        //Foreign key aanmaken met onderstaande variables.
        //[ForeignKey("userNameId")]
        //public int userNameId { get; set; }

        //public UserNameModal UserName { get; set; } = null!;
    }
}
