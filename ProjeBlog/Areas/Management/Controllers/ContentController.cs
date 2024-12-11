using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class ContentController : Controller
    {
        MyDbContext _db;
        int id;
        IRepository<AppUser> _repoUser;
        string _userId;
        public ContentController(MyDbContext db,
            IRepository<AppUser> repoUser,
            string userId)
        {
            _userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;
            _db = db;
            _repoUser  = repoUser;
        }
        public async Task<IActionResult> Index(string id)
        {
            //Authorization' dan sonra düzeltilecek
            List<Content> content = _db.Contents.Where(a => (a.Status != Enums.DataStatus.Deleted) && a.AppUserID == Int32.Parse(id)).ToList();
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
