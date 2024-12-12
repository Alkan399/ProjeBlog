using ProjeBlog.Models;
using System.Collections.Generic;

namespace ProjeBlog.RepositoryPattern.Interfaces
{
    public interface IAppUserRepository:IRepository<AppUser>
    {
        List<AppUser> GetUsersWithDetail();
        public void CreateUserWithDetails(AppUser user, AppUserDetail appUserDetail);
        public AppUser GetUserWithDetailById(int id);
        public void UpdateUserWithDetails(AppUser user);

    }
}
