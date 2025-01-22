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
using System.Threading.Tasks;
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
        IContentSetElementRepository _repoContentSetElement;
        IRepository<Statistics> _repoStatistics;
        IRepository<ContentStatistics> _repoContentStatistics;
        public BlogController(MyDbContext db,
            IContentRepository repoContent,
            IRepository<Category> repoCategory,
            IContentSetElementRepository repoContentSetElement,
            IRepository<Statistics> repoStatistics,
            IRepository<ContentStatistics> repoContentStatistics)
        {
            _db = db;
            _repoContent = repoContent;
            _repoCategory = repoCategory;
            _repoContentSetElement = repoContentSetElement;
            _repoStatistics = repoStatistics;
            _repoContentStatistics = repoContentStatistics;
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
          
        public async Task<IActionResult> ContentPage(int idx)
        {
            id = idx;
           
            Content content = _repoContent.GetContentWithUserAndStatistics(x => x.ID == id && x.Status != Enums.DataStatus.Deleted).FirstOrDefault();
            if (content == null) 
            {
                return NotFound();
            }

            if (HttpContext.Request.Method == "GET")
            {
                ContentStatistics contentStatistics = content.ContentStatistics.Where(x => x.Content.ID == id && x.CreatedDate.Date == (System.DateTime.Now).Date).FirstOrDefault();
                if (contentStatistics != null)
                {
                    contentStatistics.ViewCount += 1;
                    _db.SaveChanges();
                }
                else if(contentStatistics == null)
                {
                    ContentStatistics contentStatisticsToCreate = new ContentStatistics();
                    contentStatisticsToCreate.ViewCount += 1;
                    contentStatisticsToCreate.ContentID = id;
                    _repoContentStatistics.Add(contentStatisticsToCreate);
                }
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
