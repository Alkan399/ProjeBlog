using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class UserController : Controller
    {
        MyDbContext _db;
        IAppUserRepository _repoAppUser;
        public UserController(MyDbContext db,
            IAppUserRepository repoAppUser)
        {
            _db = db;
            _repoAppUser = repoAppUser;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserList()
        {
            List<AppUser> users = _repoAppUser.GetUsersWithDetail(
                );
            return View(users);
        }

        public IActionResult Create(AppUser user)
        {
            _repoAppUser.Add(user);
            return View();
        }
        public IActionResult Edit(int id)
        {
            AppUser appUser = _repoAppUser.GetById(id);
            return View();
        }
        [HttpPost]
        public IActionResult Edit(AppUser appUser)
        {
            _repoAppUser.Update(appUser);
            return View();
        }
        public IActionResult Delete(int id)
        {
            _repoAppUser.Delete(id);
            return View();
        }
    }
}
