using ProjeBlog.Enums;

namespace ProjeBlog.Models.Dto
{
    public class AppUserFilterDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
