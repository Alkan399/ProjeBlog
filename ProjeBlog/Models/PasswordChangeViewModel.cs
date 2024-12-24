using System.ComponentModel.DataAnnotations.Schema;

namespace ProjeBlog.Models
{
    [NotMapped]
    public class PasswordChangeViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ReNewPassword { get; set; }
    }
}
