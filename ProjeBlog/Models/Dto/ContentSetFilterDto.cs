namespace ProjeBlog.Models.Dto
{
    public class ContentSetFilterDto
    {
        public ContentSetFilterDto()
        {
            ID = null; 
            Name = null;
            Description = null;
            Status = null;
        }
        public int? ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
