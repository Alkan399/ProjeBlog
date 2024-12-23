using Microsoft.AspNetCore.Http;
using ProjeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjeBlog.RepositoryPattern.Interfaces
{
    public interface IAppUserRepository:IRepository<AppUser>
    {
        List<AppUser> GetUsersWithDetail();
        public void CreateUserWithDetails(AppUser user, AppUserDetail appUserDetail);
        public AppUser GetUserWithDetailById(int id);
        public void UpdateUserWithDetails(AppUser user);
        public List<AppUser> GetUserWithDetailsByFilter(Expression<Func<AppUser, bool>> exp);
        public AppUser GetUserWithDetailsByCookie(HttpContext httpContext);
        //AppUser GetUsersWithAllDetail();

    }
}
