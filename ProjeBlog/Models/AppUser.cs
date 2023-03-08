using ProjeBlog.Enums;

namespace ProjeBlog.Models
{
    public class AppUser:BaseEntity
    {
        string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
