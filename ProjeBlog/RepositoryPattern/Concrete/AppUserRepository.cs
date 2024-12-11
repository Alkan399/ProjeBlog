using Microsoft.EntityFrameworkCore;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Base;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjeBlog.RepositoryPattern.Concrete
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(MyDbContext db) : base(db)
        {
        }

        public List<AppUser> GetUsersWithDetail()
        {
            return table.Include(d => d.AppUserDetail).Include(e => e.Contents).ToList();
        }
    }
}
