using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.Models.Dto;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList.Extensions;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class ContentController : Controller
    {
        MyDbContext _db;
        int id;
        IRepository<AppUser> _repoUser;
        IContentRepository _repoContent;
        public ContentController(MyDbContext db,
            IRepository<AppUser> repoUser,
            IContentRepository repoContent)
        {
            _db = db;
            _repoUser  = repoUser;
            _repoContent = repoContent;

        }
        HttpContext _httpContext;
        public  IActionResult Index()
        {
            int userId = _repoUser.GetUserId(HttpContext);
            //Authorization' dan sonra düzeltilecek
            List<Content> content = _db.Contents.Where(a => (a.Status != Enums.DataStatus.Deleted) && a.AppUserID == userId).ToList();
            return View(content);
        }
        public IActionResult ListAllContents()
        {
            int userId = _repoUser.GetUserId(HttpContext);
            //Authorization' dan sonra düzeltilecek
            List<Content> content = _db.Contents.Where(a => (a.Status != Enums.DataStatus.Deleted) && a.AppUserID == userId).ToList();
            return View(content);
        }
        public IActionResult Update(int id)
        {
            Content content = _repoContent.GetById(id);

            return View(content);
        }
        [HttpPost]
        public IActionResult Update(Content content)
        {
            string msg = "Kayıt güncelleme BAŞARISIZ!";
            content.AppUserID = _repoUser.GetUserId(HttpContext);
            _repoContent.Update(content);
            Content c = _repoContent.GetById(content.ID);
            if(content.Title == c.Title && content.CoverImagePath == c.CoverImagePath && content.Entry == c.Entry)
            {
                msg = "Kayıt güncelleme BAŞARILI!";
                ViewData["Message"] = msg;
            }
            else
            {
                ViewData["Message"] = msg;
            }
            return View(content);
        }   
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Content content)
        {
            content.AppUserID = _repoUser.GetUserId(HttpContext);
            _repoContent.Add(content);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _repoContent.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteManagement(int id)
        {
            _repoContent.Delete(id);
            return RedirectToAction("ListAllContents");
        }
        public IActionResult Details(int id)
        {
            Content content = _repoContent.GetById(id);
            return View(content);
        }
        public IActionResult DetailsManagement(int id)
        {
            Content content = _repoContent.GetById(id);
            return View(content);
        }
        public IActionResult FilterContents([FromBody] ContentFilterDto criteria, int page)
        {
            Expression<Func<Content, bool>> filterExpression = content =>
                (string.IsNullOrEmpty(criteria.UserName) || content.AppUser.UserName.Contains(criteria.UserName)) &&
                (string.IsNullOrEmpty(criteria.Title) || content.Title.Contains(criteria.Title)) &&
                (string.IsNullOrEmpty(criteria.Entry) || content.Entry.Contains(criteria.Entry)) &&
                (string.IsNullOrEmpty(criteria.Status) || (criteria.Status == "Active"
            ? content.Status == Enums.DataStatus.Inserted || content.Status == Enums.DataStatus.Updated
            : content.Status == Enums.DataStatus.Deleted));

            var filteredContents = _repoContent.GetContentWithUser(filterExpression).ToPagedList(page,2);

            var jsonResponse = new
            {
                PageNumber = filteredContents.PageNumber, // Mevcut sayfa
                PageCount = filteredContents.PageCount,   // Toplam sayfa
                HasNextPage = filteredContents.HasNextPage, // Sonraki sayfa var mı
                HasPreviousPage = filteredContents.HasPreviousPage, // Önceki sayfa var mı
                Contents = filteredContents // Sayfa içerikleri
            };

            var serializedResponse = JsonConvert.SerializeObject(jsonResponse, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            });

            return Content(serializedResponse, "application/json");
        }
    }
}
