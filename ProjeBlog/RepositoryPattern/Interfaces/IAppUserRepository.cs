using ProjeBlog.Models;
using System.Collections.Generic;

namespace ProjeBlog.RepositoryPattern.Interfaces
{
    public interface IAppUserRepository:IRepository<AppUser>
    {
        List<AppUser> GetUsersWithDetail();
    }
}
