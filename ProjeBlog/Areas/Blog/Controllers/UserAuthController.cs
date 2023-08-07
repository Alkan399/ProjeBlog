using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Models;

namespace ProjeBlog.Areas.Blog.Controllers
{
    public class UserAuthController : Controller
    {
        [Area("Blog")]


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AppUser user)
        {
            return View();
        }
    }
}
