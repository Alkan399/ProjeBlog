using System.Diagnostics.CodeAnalysis;

namespace ProjeBlog.Models.Dto
{
    public class ContentFilterDto
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Entry { get; set; }
        public string Status { get; set; }
        public int ItemsPerPage { get; set; }
        public string Category { get; set; }
    }
}
