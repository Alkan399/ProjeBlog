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
            List<Content> contents = _repoContent.GetAll().Where(x => x.Status != Enums.DataStatus.Deleted).ToList();
            return View(contents);
        }
          
        public IActionResult ContentPage(int idx)
        {
            id = idx;
           
            List<Content> contents = _repoContent.GetByFilter(x => x.ID == id && x.Status != Enums.DataStatus.Deleted);
            if (contents.Count < 1) 
            {
                return NotFound();
            }
            Content content = contents[0];

            if (HttpContext.Request.Method == "GET")
            {
                content.Views += 1;
                _db.SaveChanges();
            }

            return View(content);
        }
        
    }
}
