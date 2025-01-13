using ProjeBlog.Models;
using System.Collections.Generic;

namespace ProjeBlog.RepositoryPattern.Interfaces
{
    public interface IUtilityMethods
    {
        public string BlogContentFormatting(Content content);
        public List<ContentSetElement> ContentSetElements(Enums.ElementLocation location);
    }
}
