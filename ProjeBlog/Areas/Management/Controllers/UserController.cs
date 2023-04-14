using Microsoft.AspNetCore.Mvc;
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
            List<AppUser> users = _db.Users.ToList();
            return View(users);
        }
    }
}
