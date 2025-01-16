using System.ComponentModel.DataAnnotations;

namespace ProjeBlog.Models.ConstantModels
{
    public class About : BaseEntity
    {
        public About()
        {
            Entry = "";
            Status = Enums.DataStatus.Inserted;
        }
        [Required]
        public string Entry { get; set; }
    }
}
