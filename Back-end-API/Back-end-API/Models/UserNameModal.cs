using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_API.Models
{
    public class UserNameModal
    {
        public int UserNameId { get; set; }

        public string Name { get; set; } = null!;
    }
}
