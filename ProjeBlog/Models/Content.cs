using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjeBlog.Models
{
    public class Content: BaseEntity
    {   [Column(TypeName ="text")]
        public string Entry { get; set; }
        [Column(TypeName ="nvarchar(30)"),Required]
        public string Title { get; set; }

        public int AppUserID { get; set; }
        public string CoverImagePath { get; set; }
        public int Views { get; set; } = 0;

        //Relational Property

        public virtual AppUser AppUser { get; set; }

    }
}
