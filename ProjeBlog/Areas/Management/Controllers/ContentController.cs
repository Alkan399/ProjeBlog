using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            //Authorization' dan sonra düzeltilecek
            List<Content> content = _db.Contents.Where(a => (a.Status != Enums.DataStatus.Deleted) && a.AppUserID == 1 ).ToList();
            return View(content);
        }
        public IActionResult Update(int id)
        {
            Content content = _db.Contents.Find(id);

            return View(content);
        }
        [HttpPost]
        public IActionResult Update(Content content)
        {
            content.AppUserID = 1;
            content.Status = Enums.DataStatus.Updated;
            content.UpdatedDate = DateTime.Now;
            _db.Contents.Update(content);
            _db.SaveChanges();
            return RedirectToAction("Blog");
        }
        public IActionResult Create()
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
