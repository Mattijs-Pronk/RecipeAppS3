namespace Back_end_API.BusinessLogic.UserDTO_s
{
    public class ChangePasswordDTO
    {
        public int userId { get; set; }

        public string currentPassword { get; set; } = string.Empty;

        public string newPassword { get; set; } = string.Empty;
    }
}
