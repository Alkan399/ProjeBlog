using ProjeBlog.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace ProjeBlog.RepositoryPattern.Interfaces
{
    public interface IContentRepository : IRepository<Content>
    {
        List<Content> GetUserWithDetailsByFilter(Expression<Func<Content, bool>> exp);
    }
}
