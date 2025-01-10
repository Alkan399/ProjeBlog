using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.Models.Dto;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using X.PagedList.Extensions;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class ContentSetContentController : Controller
    {
        MyDbContext _db;
        IContentSetContentRepository _repoContentSetContent;
        public ContentSetContentController(MyDbContext db, IContentSetContentRepository repoContentSetContent)
        {
            _db = db;
            _repoContentSetContent = repoContentSetContent;
        }
        public IActionResult Delete(int id)
        {
            _repoContentSetContent.SpecialDelete(id);
            return RedirectToAction("Index");
        }
        public IActionResult Create(int ContentSetId)
        {
            ContentSetContent contentSetContent = new ContentSetContent();
            contentSetContent.ContentSetID = ContentSetId;
            return View(contentSetContent);
        }
        [HttpPost]
        public IActionResult Create(ContentSetContent contentSetContent)
        {
            if (!ModelState.IsValid)
            {
                return View(contentSetContent);
            }
            _repoContentSetContent.Add(contentSetContent);
            return View(contentSetContent);
        }
        public IActionResult Index(int id)
        {
            ContentSetContentFilterDto criteria = new ContentSetContentFilterDto();
            criteria.ContentSetID = id;
            Expression<Func<ContentSetContent, bool>> filterExpression = contentSetContent =>
                criteria.Order == 0 || criteria.Order == null || criteria.Order == contentSetContent.Order &&
                criteria.ContentID == 0 || criteria.ContentID == null || criteria.ContentID == contentSetContent.ContentID &&
                criteria.ContentSetID == 0 || criteria.ContentSetID == null || criteria.ContentSetID == contentSetContent.ContentSetID &&
                (string.IsNullOrEmpty(criteria.Status) || (criteria.Status == "Active"
            ? contentSetContent.Status == Enums.DataStatus.Inserted || contentSetContent.Status == Enums.DataStatus.Updated
            : contentSetContent.Status == Enums.DataStatus.Deleted));

            ViewData["ContentSetId"] = id;
            List<ContentSetContent> contentSetContents = _repoContentSetContent.GetContentSetContentWithAllRelationsByFilter(filterExpression).OrderByDescending(contentsetcontent => contentsetcontent.Order).ToList();
            return View(contentSetContents);
        }
        public IActionResult FilterContents([FromBody] ContentSetContentFilterDto criteria, [FromQuery] int page)
        {

            Expression<Func<ContentSetContent, bool>> filterExpression = contentSetContent =>
                criteria.Order == 0 || criteria.Order == null || criteria.Order == contentSetContent.Order &&
                criteria.ContentID == 0 || criteria.ContentID == null || criteria.ContentID == contentSetContent.ContentID &&
                criteria.ContentSetID == 0 || criteria.ContentSetID == null || criteria.ContentSetID == contentSetContent.ContentSetID &&
                (string.IsNullOrEmpty(criteria.Status) || (criteria.Status == "Active"
            ? contentSetContent.Status == Enums.DataStatus.Inserted || contentSetContent.Status == Enums.DataStatus.Updated
            : contentSetContent.Status == Enums.DataStatus.Deleted));

            var filteredContents = _repoContentSetContent.GetByFilter(filterExpression).ToPagedList(page, criteria.ItemsPerPage);

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
