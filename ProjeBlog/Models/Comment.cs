namespace ProjeBlog.Models
{
    public class Comment:BaseEntity
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string CommentContent { get; set; }
        public virtual Content Content { get; set; }
        public virtual AppUser User { get; set; }
    }
}
