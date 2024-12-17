using Microsoft.EntityFrameworkCore;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Base;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;

namespace ProjeBlog.RepositoryPattern.Concrete
{
    public class ContentRepository : Repository<Content>, IContentRepository
    {
        protected DbSet<AppUser> tableAppUsers;
        protected DbSet<Category> tableCategories;
        public ContentRepository(MyDbContext db) : base(db) 
        { 
            tableAppUsers = db.Set<AppUser>();
            tableCategories = db.Set<Category>();
        }
        public List<Content> GetContentWithUser(Expression<Func<Content, bool>> exp)
        { 
            return table.Where(exp).Include(x => x.AppUser).Include(x => x.Category).ToList();
        }
        public List<Content> GetAllContent()
        {
            return table.Include(x => x.AppUser).Include(x => x.Category).ToList();
        }

        public List<Category> GetCategories()
        {
            return tableCategories.ToList();
        }
    }
}
