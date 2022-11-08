using System.ComponentModel.DataAnnotations;

namespace Back_end_API.BusinessLogic
{
    public class CreateUserDTO
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
