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
        public IActionResult Blog()
        {
            List<Content> content = _repoContent.GetAll().Where(x => x.Status != Enums.DataStatus.Deleted).ToList();
            return View(content);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Content content)
        {
            content.AppUserID = 4;
            _repoContent.Add(content);
            return RedirectToAction("Blog");
        }

        public IActionResult Update(int id)
        {
            Content content = _repoContent.GetById(id);

            return View(content);
        }
        [HttpPost]
        public IActionResult Update(Content content)
        {
            content.AppUserID = 4;
            _repoContent.Update(content);
            return RedirectToAction("Blog");
        }

        public IActionResult Delete(int id)
        {
            _repoContent.Delete(id);
            return RedirectToAction("Blog");
        }
        [Route("/{idx}")]   
        public IActionResult ContentPage(int idx)
        {
            id = idx;
            //ViewBag.Id = id.ToString();

            List<Content> content = _db.Contents.Where(x => x.ID == id && x.Status != Enums.DataStatus.Deleted).ToList();
            if (content.Count > 0)
            {
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
