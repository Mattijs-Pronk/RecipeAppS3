namespace Back_end_API.BusinessLogic
{
    public class ResetUserPasswordDTO
    {
        public string passwordResetToken { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
