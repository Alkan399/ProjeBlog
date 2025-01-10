using Org.BouncyCastle.Bcpg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjeBlog.Models
{
    public class ContentSet : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        // ContentSet'in birden fazla ContentSetContent ilişkisi olacak
        public virtual List<ContentSetContent> ContentSetContents { get; set; }
    }
}
