using System.ComponentModel.DataAnnotations;

namespace Back_end_API.BusinessLogic
{
    public class UserDTO
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool isAdmin { get; set; }
    }
}
