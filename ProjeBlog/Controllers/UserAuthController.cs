using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Models;

namespace ProjeBlog.Controllers
{
    public class UserAuthController : Controller
    {
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
