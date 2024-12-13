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
        public ContentRepository(MyDbContext db) : base(db) 
        { 
            tableAppUsers = db.Set<AppUser>();
        }
        public List<Content> GetUserWithDetailsByFilter(Expression<Func<Content, bool>> exp)
        {
            return table.Where(exp).Include(x => x.AppUser).ToList();
        }
    }
}
