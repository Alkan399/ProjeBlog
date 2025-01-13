using ProjeBlog.Models;
using System.Collections.Generic;

namespace ProjeBlog.RepositoryPattern.Interfaces
{
    public interface IContentSetElementRepository : IRepository<ContentSetElement>
    {
        public ContentSetElement GetContentSetElementWithcontentSetById(int id);
        public List<ContentSetElement> GetContentSetElementsWithContentSets();
        public List<ContentSetElement> GetContentSetElementsWithContentSetsWithContents(Enums.ElementLocation loc);

    }
}
