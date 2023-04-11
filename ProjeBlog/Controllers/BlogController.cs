using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjeBlog.Controllers
{
    public class BlogController : Controller
    {
        MyDbContext _db;
        int id;
        public BlogController(MyDbContext db)
        {
            _db = db;
        }
        public IActionResult Blog()
        {
            List<Content> content = _db.Contents.Where(a => a.Status != Enums.DataStatus.Deleted).ToList();
            return View(content);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Content content)
        {
            content.AppUserID = 7;
            _db.Contents.Add(content);
            _db.SaveChanges();
            return RedirectToAction("Blog");
        }

        public IActionResult Update(int id)
        {
            Content content = _db.Contents.Find(id);
            
            return View(content);
        }
        [HttpPost]
        public IActionResult Update(Content content)
        {
            content.AppUserID = 7;
            content.Status = Enums.DataStatus.Updated;
            content.UpdatedDate = DateTime.Now;
            _db.Contents.Update(content);
            _db.SaveChanges();
            return RedirectToAction("Blog");
        }

        public IActionResult Delete(int id)
        {
            Content content = _db.Contents.Find(id);
            content.Status = Enums.DataStatus.Deleted;
            content.UpdatedDate = DateTime.Now;
            _db.Contents.Update(content);
            _db.SaveChanges();
            return RedirectToAction("Blog");
        }
        [Route("/{idx}")]
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
