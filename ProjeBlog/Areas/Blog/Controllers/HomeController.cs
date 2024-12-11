using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Collections.Generic;

namespace ProjeBlog.Areas.Blog.Controllers
{
    [Area("Blog")]

    public class HomeController : Controller
    {
        MyDbContext _db;
        int id;
        IRepository<Content> _repoContent;
        public HomeController(MyDbContext db, 
            IRepository<Content> repository)
        {
            _db = db;
            _repoContent = repository;
        }
        public IActionResult Index()
        {
            List<Content> content = _repoContent.GetAll();
            return View(content);
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
            _db.Basvurus.Add(basvuru);
            _db.SaveChanges();
            return RedirectToAction("Basvur");
        }
    }
}
