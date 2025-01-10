using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using ProjeBlog.Enums;

namespace ProjeBlog.Models
{
    public class ContentSetElement:BaseEntity
    {
        [Required]
        public ElementLocation Location { get; set; }
        public string ElementID { get; set; }
        public int? ContentSetID { get; set; }
        public bool Recent { get; set; }
        public bool MostPopular { get; set; }
        public int ShowCount { get; set; }
        public virtual ContentSet ContentSet { get; set; }
    }
}
