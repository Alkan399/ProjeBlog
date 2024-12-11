using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjeBlog.Areas.Blog.Controllers
{
    [Area("Blog")]

    public class UserAuthController : Controller
    {
        IRepository<AppUser> _repoUser;
        public UserAuthController(IRepository<AppUser> repoUser)
        {
            _repoUser = repoUser;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Login(AppUser user)
        {
            if (_repoUser.Any(x => x.UserName == user.UserName && x.Status != Enums.DataStatus.Deleted)) 
            {
                AppUser selectedUser = _repoUser.Default(x => x.UserName == user.UserName && x.Status != Enums.DataStatus.Deleted);
                bool isValid = BCrypt.Net.BCrypt.Verify(user.Password, selectedUser.Password);
                if (isValid)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim("userName", selectedUser.UserName),
                        new Claim("userId", selectedUser.ID.ToString()),
                        new Claim("role", selectedUser.Role.ToString())
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                    if (selectedUser.Role == Enums.Role.Admin) 
                    {
                        return Redirect("/Management/Home/Index");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Blog/Home/Index");
        }
    }
}
