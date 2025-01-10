using ProjeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjeBlog.RepositoryPattern.Interfaces
{
    public interface IContentSetContentRepository : IRepository<ContentSetContent>
    {
        public List<ContentSetContent> GetContentSetContentWithAllRelationsByFilter(Expression<Func<ContentSetContent, bool>> exp);
    }
}
