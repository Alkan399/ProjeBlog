using System.ComponentModel.DataAnnotations;

namespace ProjeBlog.Models
{
    public class ContentSetContent : BaseEntity
    {
        [Required]
        public int ContentID { get; set; }  // İçeriğin ID'si
        [Required]
        public int ContentSetID { get; set; }  // ContentSet'in ID'si
        [Required]
        public int Order { get; set; }  // İçeriğin sırası

        // İlişkiler
        public virtual ContentSet ContentSet { get; set; }  // ContentSet ilişkisi
        public virtual Content Content { get; set; }  // Content ilişkisi
    }

}
