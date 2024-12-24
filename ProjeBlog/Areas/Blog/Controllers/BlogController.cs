using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjeBlog.Areas.Blog.Controllers
{
    [Area("Blog")]

    public class BlogController : Controller
    {
        MyDbContext _db;
        int id;
        IRepository<Content> _repoContent;
        public BlogController(MyDbContext db,
            IRepository<Content> repoContent)
        {
            _db = db;
            _repoContent = repoContent;
        }
        public IActionResult Index()
        {
            List<Content> content = _repoContent.GetAll().Where(x => x.Status != Enums.DataStatus.Deleted).ToList();
            return View(content);
        }
        [Route("/{idx}")]   
        public IActionResult ContentPage(int idx)
        {
            id = idx;
            //ViewBag.Id = id.ToString();

            List<Content> content = _db.Contents.Where(x => x.ID == id && x.Status != Enums.DataStatus.Deleted).ToList();
            
            if (content.Count > 0)
            {
                content[0].Views += 1;
                _db.SaveChanges();
                return View(content);
            }
            else 
            {
                //return RedirectToAction("Blog");
                return NotFound();
            }
        }
        [HttpGet]
        public IActionResult ContentPage()
        {
            var c = _db.Contents.Where(x => x.ID == id).ToList();

            return View(c.ToList());

        }

    }
}
