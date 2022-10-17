using System.ComponentModel.DataAnnotations;

namespace Back_end_API.BusinessLogic
{
    public class UserDTO
    {
        public string email { get; set; } = null!;

        public string password { get; set; } = null!;
    }
}
