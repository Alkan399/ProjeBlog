using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]

    public class ContentController : Controller
    {
        MyDbContext _db;
        int id;
        public ContentController(MyDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Content content)
        {
            content.AppUserID = 1;
            _db.Contents.Add(content);
            _db.SaveChanges();
            return RedirectToAction("Blog");
        }
    }
}
