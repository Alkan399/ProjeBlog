using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.Models.Dto;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Linq.Expressions;
using System;
using X.PagedList.Extensions;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class ContentSetController : Controller
    {
        MyDbContext _db;
        IRepository<ContentSet> _repoContentSet;
        public ContentSetController(MyDbContext db, IRepository<ContentSet> repoContentSet)
        {
            _db = db;
            _repoContentSet = repoContentSet;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContentSet contentSet)
        {
            if (!ModelState.IsValid)
            {
                return View(contentSet);
            }
            _repoContentSet.Add(contentSet);
            return View(contentSet);
        }
        
        public IActionResult Update(int id)
        {
            ContentSet contentSet = _repoContentSet.GetById(id);
            return View(contentSet);
        }
        [HttpPost]
        public IActionResult Update(ContentSet contentSet)
        {
            if (!ModelState.IsValid)
            {
                return View(contentSet);
            }
            _repoContentSet.Update(contentSet);
            return View(contentSet);
        }
        public IActionResult Index()
        {
            List<ContentSet> contentSets = new List<ContentSet>();
            return View(contentSets);
        }
        public IActionResult FilterContents([FromBody] ContentSetFilterDto criteria, [FromQuery] int page)
        {

            Expression<Func<ContentSet, bool>> filterExpression = contentSet =>
                (string.IsNullOrEmpty(criteria.Name) || contentSet.Name.Contains(criteria.Name)) &&
                (string.IsNullOrEmpty(criteria.Description) || contentSet.Description.Contains(criteria.Description)) &&
                criteria.ID == 0 || criteria.ID == null || criteria.ID == contentSet.ID &&
                (string.IsNullOrEmpty(criteria.Status) || (criteria.Status == "Active"
            ? contentSet.Status == Enums.DataStatus.Inserted || contentSet.Status == Enums.DataStatus.Updated
            : contentSet.Status == Enums.DataStatus.Deleted));

            var filteredContents = _repoContentSet.GetByFilter(filterExpression).ToPagedList(page, 5/*criteria.ItemsPerPage*/);

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
