namespace Back_end_API.BusinessLogic.UserDTO_s
{
    public class ChangeUserProfileDTO
    {
        public int userId { get; set; }

        public string userName { get; set; } = string.Empty;

        public string adress { get; set; } = null!;

        public string phone { get; set; } = null!;
    }
}
