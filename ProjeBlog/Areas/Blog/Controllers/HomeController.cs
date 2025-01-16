using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.Models.ConstantModels;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ProjeBlog.Areas.Blog.Controllers
{
    [Area("Blog")]

    public class HomeController : Controller
    {
        MyDbContext _db;
        int id;
        IRepository<Content> _repoContent;
        IRepository<About> _repoAbout;
        public HomeController(MyDbContext db, 
            IRepository<Content> repoContent,
            IRepository<About> repoAbout)
        {
            _db = db;
            _repoContent = repoContent;
            _repoAbout = repoAbout;
        }
        public IActionResult Index()
        {
            List<Content> content = _repoContent.GetAll().Where(a=> a.Status != Enums.DataStatus.Deleted).ToList();
            return View(content);
        }
        public IActionResult About()
        {
            About about = _repoAbout.GetActives().FirstOrDefault();
            return View(about);
        }
        public IActionResult Contact()
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
