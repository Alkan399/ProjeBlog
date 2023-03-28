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

        //Relational Property

        public virtual AppUser AppUser { get; set; }

    }
}
