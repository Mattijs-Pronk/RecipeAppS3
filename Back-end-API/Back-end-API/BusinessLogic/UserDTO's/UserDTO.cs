namespace Back_end_API.BusinessLogic.UserDTO_s
{
    public class UserDTO
    {
        public int userId { get; set; }

        public string userName { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;

        public bool isAdmin { get; set; }

        public string adress { get; set; } = string.Empty;

        public string phone { get; set; } = string.Empty;

        public string activateAccountToken { get; set; } = string.Empty;

        public string passwordResetToken { get; set; } = string.Empty;

    }
}
