using ProjeBlog.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace ProjeBlog.RepositoryPattern.Interfaces
{
    public interface IContentRepository : IRepository<Content>
    {
        List<Content> GetContentWithUser(Expression<Func<Content, bool>> exp);
        List<Content> GetAllContent();
        List<Category> GetCategories();
    }
}
