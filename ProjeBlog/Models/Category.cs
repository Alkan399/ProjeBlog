using System.Collections.Generic;

namespace ProjeBlog.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual List<Content> Content { get; set; }
    }
}
