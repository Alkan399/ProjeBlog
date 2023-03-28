using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders.Composite;
using ProjeBlog.Context;
using ProjeBlog.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjeBlog.Controllers
{
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
        [HttpPost("{idx}")]
        public IActionResult ContentPage(int idx) 
        {
            id = idx;
            //ViewBag.Id = id.ToString();

            List<Content> content = _db.Contents.Where(x => x.ID == id).ToList();
            return View(content);
        }
        [HttpGet]
        public IActionResult ContentPage()
        {
            /*var c = _db.Contents.Where(x => x.ID == id).ToList();

            return View(c.ToList());*/
            return View();

        }


    }
}
