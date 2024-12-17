namespace ProjeBlog.Models.Dto
{
    public class CategoryFilterDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string Status { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
