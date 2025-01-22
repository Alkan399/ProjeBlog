using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace ProjeBlog.Models
{
    public class ContentStatistics:BaseEntity
    {
        public ContentStatistics()
        {
            ViewCount = 0;
            Date = DateTime.Now.Date;
        }
        public int ViewCount {get; set; }
        public DateTime Date {get; set; }
        public int ContentID {get; set; }
        public virtual Content Content {get; set; }
    }
}
