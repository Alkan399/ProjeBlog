using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Base;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjeBlog.RepositoryPattern.Concrete
{
    public class ContentSetElementRepository : Repository<ContentSetElement> ,IContentSetElementRepository
    {
        protected DbSet<ContentSetElement> tableContentSetElement;
        protected DbSet<ContentSet> tableContentSet;
        public ContentSetElementRepository(MyDbContext db) : base(db)
        {
            tableContentSetElement = db.Set<ContentSetElement>();
            tableContentSet = db.Set<ContentSet>();
        }
        public ContentSetElement GetContentSetElementWithcontentSetById(int id)
        {
            return table.Include(d => d.ContentSet).Where(x => x.ID == id).FirstOrDefault();
        }
        public List<ContentSetElement> GetContentSetElementsWithContentSets()
        {
            return table.Include(d => d.ContentSet).ToList();
        }
        public List<ContentSetElement> GetContentSetElementsWithContentSetsWithContents(Enums.ElementLocation loc)
        {
            return table
                .Include(d => d.ContentSet)
                    .ThenInclude(e => e.ContentSetContents)
                    .ThenInclude(f => f.Content)
                .Where(x => x.Location == loc).ToList();
        }
    }
}
