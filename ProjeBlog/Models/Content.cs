namespace ProjeBlog.Models
{
    public class Content: BaseEntity
    {
        public string Entry { get; set; }
        public string Title { get; set; }
        public string AuthorID { get; set; }

    }
}
