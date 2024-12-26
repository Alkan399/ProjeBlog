using System.Diagnostics.CodeAnalysis;

namespace ProjeBlog.Models.Dto
{
    public class ContentFilterDto
    {
        public ContentFilterDto()
        {
            UserId = null;
            UserName = null;
            Title = null;
            Entry = null;
            Status = null;
            ItemsPerPage = 10;
            Category = null;
        }

        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Entry { get; set; }
        public string Status { get; set; }
        public int ItemsPerPage { get; set; }
        public string Category { get; set; }
    }
}
