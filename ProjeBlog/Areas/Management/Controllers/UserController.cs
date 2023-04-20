using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeBlog.Context;
using ProjeBlog.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    public class UserController : Controller
    {
        MyDbContext _db;
        public UserController(MyDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserList()
        {
            List<AppUser> users = _db.Users.Include(d => d.AppUserDetail).Include(e => e.Contents).ToList();
            return View(users);
        }

        public IActionResult Create(AppUser user)
        {
            _db.Users.Add(user);
            return View();
        }
    }
}
