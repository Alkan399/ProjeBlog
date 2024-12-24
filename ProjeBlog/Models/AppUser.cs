using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjeBlog.Models
{
    public class AppUser:BaseEntity
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [NotMapped]
        public string RePassword { get; set; }
        [Required]
        public string Email { get; set; }
        public Role Role { get; set; }

        // Relational Property
        public virtual AppUserDetail AppUserDetail { get; set; }
        public virtual List<Content> Contents { get; set; }
        public virtual Basvuru Basvuru { get; set; }
        public virtual List<Comment> Comment { get; set; }
    }
}
