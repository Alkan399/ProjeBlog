using Microsoft.EntityFrameworkCore;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Base;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProjeBlog.RepositoryPattern.Concrete
{
    public class ContentSetContentRepository : Repository<ContentSetContent>, IContentSetContentRepository
    {
        protected DbSet<ContentSetContent> tableContentSetContent;
        protected DbSet<ContentSet> tableContentSet;
        protected DbSet<Content> tableContent;
        public ContentSetContentRepository(MyDbContext db) : base(db)
        {
            tableContentSetContent = db.Set<ContentSetContent>();
        }
        public List<ContentSetContent> GetContentSetContentWithAllRelationsByFilter(Expression<Func<ContentSetContent, bool>> exp)
        {
            return table.Where(exp).Include(d => d.Content).Include(e => e.ContentSet).ToList();
        }
    }
}
