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
using X.PagedList;
using X.PagedList.Extensions;

namespace ProjeBlog.Areas.Blog.Controllers
{
    [Area("Blog")]

    public class BlogController : Controller
    {
        MyDbContext _db;
        int id;
        IContentRepository _repoContent;
        IRepository<Category> _repoCategory;
        public BlogController(MyDbContext db,
            IContentRepository repoContent,
            IRepository<Category> repoCategory)
        {
            _db = db;
            _repoContent = repoContent;
            _repoCategory = repoCategory;
        }
        public IActionResult Index(string filterTitle, string filterEntry, string filterCategory, int page, int itemsPerPage)
        {
            if (itemsPerPage == 0)
            {
                itemsPerPage = 5;
            }
            if(filterCategory == "null")
            {
                filterCategory = null;
            }
            ContentFilterDto filterDto = new ContentFilterDto();
            filterDto.ItemsPerPage = itemsPerPage;
            filterDto.Title = filterTitle;
            filterDto.Entry = filterEntry;
            filterDto.Category = filterCategory;
            ViewData["Categories"] = _repoCategory.GetActives();
            ViewData["SelectedCategory"] = filterCategory;

            var fc = FilterContents(filterDto, page);

            ViewData["PageCount"] = fc.PageCount;
            ViewData["CurrentPage"] = fc.PageNumber;
            ViewData["HasPreviousPage"] = fc.HasPreviousPage;
            ViewData["HasNextPage"] = fc.HasNextPage;
            List<Content> contents = fc.ToList();

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
        public IPagedList<Content> FilterContents([FromBody] ContentFilterDto criteria, [FromQuery] int page)
        {

            Expression<Func<Content, bool>> filterExpression = content =>
                (string.IsNullOrEmpty(criteria.UserName) || content.AppUser.UserName.Contains(criteria.UserName)) &&
                (string.IsNullOrEmpty(criteria.Title) || content.Title.Contains(criteria.Title)) &&
                (string.IsNullOrEmpty(criteria.Entry) || content.Entry.Contains(criteria.Entry)) &&
                (string.IsNullOrEmpty(criteria.Category) || (content.Category.ID).ToString().Equals(criteria.Category)) &&
                (content.Status == Enums.DataStatus.Inserted || content.Status == Enums.DataStatus.Updated);

            var filteredContents = _repoContent.GetContentWithUser(filterExpression).ToPagedList(page, criteria.ItemsPerPage);

                
            return filteredContents;
        }
    }
}
