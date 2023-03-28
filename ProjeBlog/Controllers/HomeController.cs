using Microsoft.AspNetCore.Mvc;

namespace ProjeBlog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ContentPage()
        {
            return View();
        }
    }
}
