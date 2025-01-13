using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using ProjeBlog.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjeBlog.Models
{
    public class ContentSetElement:BaseEntity
    {
        public ContentSetElement()
        {
            Location = ElementLocation.None;
            Custom = false;
            Recent = false;
            MostPopular = false;
            ShowCount = 0;
            ContentSet = null;
        }
        [Required]
        public ElementLocation? Location { get; set; }
        public string ElementID { get; set; }
        public int? ContentSetID { get; set; }
        public bool Custom { get; set; }    
        public bool Recent { get; set; }
        public bool MostPopular { get; set; }
        public int ShowCount { get; set; }
        [NotMapped]
        public string ContentSetOption { get; set; }
        public virtual ContentSet ContentSet { get; set; }
    }
}
