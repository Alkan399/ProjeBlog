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
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        protected DbSet<AppUserDetail> tableUserDetails;
        public AppUserRepository(MyDbContext db) : base(db)
        {
            tableUserDetails = db.Set<AppUserDetail>();
        }

        public List<AppUser> GetUsersWithDetail()
        {
            return table.Include(d => d.AppUserDetail).Include(e => e.Contents).ToList();
        }
        public void CreateUserWithDetails(AppUser user, AppUserDetail appUserDetail)
        {
            table.Add(user);
            appUserDetail.AppUserID = user.ID;
            tableUserDetails.Add(appUserDetail);
            Save();
        }

        public AppUser GetUserWithDetailById(int id)
        {
            return table.Include(d => d.AppUserDetail).Where(x => x.ID == id).FirstOrDefault();
        }

        public void UpdateUserWithDetails(AppUser user)
        {
            AppUserDetail userDetail = user.AppUserDetail;
            user.AppUserDetail = null;
            user.Status = Enums.DataStatus.Updated;
            user.UpdatedDate = DateTime.Now;
            tableUserDetails.Update(userDetail);
            table.Update(user);
            Save();
        }
        public List<AppUser> GetUserWithDetailsByFilter(Expression<Func<AppUser, bool>> exp)
        {
            return table.Where(exp).Include(x => x.AppUserDetail).ToList();
        }
    }
}
