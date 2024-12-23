using System;
using System.ComponentModel.DataAnnotations;

namespace ProjeBlog.Models
{
    public class AppUserDetail : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }
        public int AppUserID { get; set; }

        // Relational Property
        public AppUser AppUser { get; set; }
    }
}
