using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProjeBlog.Models
{
    public class Content: BaseEntity
    {
        public string Entry { get; set; }
        [Column(TypeName ="nvarchar(60)"),Required]
        public string Title { get; set; }
        public int AppUserID { get; set; }
        public int CategoryID { get; set; }
        public string CoverImagePath { get; set; }
        public int Views { get; set; } = 0;

        //Relational Property

        public virtual AppUser AppUser { get; set; }
        public virtual List<Comment> Comment { get; set; }
        public virtual Category Category { get; set; }

    }
}
