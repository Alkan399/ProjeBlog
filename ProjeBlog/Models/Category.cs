using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjeBlog.Models
{
    public class Category:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual List<Content> Content { get; set; }
    }
}
