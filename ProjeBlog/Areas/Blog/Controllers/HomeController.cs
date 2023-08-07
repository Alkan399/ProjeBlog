using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models;

namespace ProjeBlog.Areas.Blog.Controllers
{
    [Area("Blog")]

    public class HomeController : Controller
    {
        MyDbContext _db;
        int id;
        public HomeController(MyDbContext db)
        {
            _db = db;
        }
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
        [HttpGet]
        public IActionResult Basvur()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Basvur(Basvuru basvuru)
        {
            _db.Basvuru.Add(basvuru);
            _db.SaveChanges();
            return RedirectToAction("Basvur");
        }
    }
}
