using System.ComponentModel.DataAnnotations;

namespace ProjeBlog.Models.Dto
{
    public class ContentSetContentFilterDto
    {
        public ContentSetContentFilterDto()
        {
            ContentID = null;
            ContentSetID = null;
            Order = null;
            Status = null;
            ItemsPerPage = 5;
        }
        public int? ContentID { get; set; }  // İçeriğin ID'si
        public int? ContentSetID { get; set; }  // ContentSet'in ID'si
        public int? Order { get; set; }  // İçeriğin sırası
        public string Status { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
