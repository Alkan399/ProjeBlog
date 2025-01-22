using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.Models.Dto;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using X.PagedList.Extensions;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class CategoryController : Controller
    {
        MyDbContext _db;
        IRepository<Category> _repoCategory;
        public CategoryController(IRepository<Category> repoCategory, MyDbContext db)
        {
            _db = db;
            _repoCategory = repoCategory;
        }
        public IActionResult Index()
        {
            List<Category> categories = _repoCategory.GetAll();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            string msg = "Kayıt ekleme BAŞARISIZ!";
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _repoCategory.Add(category);
            Category ControlObject = _repoCategory.GetById(category.ID);
            if (category.Name == ControlObject.Name && category.Description == ControlObject.Description)
            {
                msg = "Kayıt başarıyla eklendi! ("+category.Name+")";
                ViewData["Message"] = msg;
            }
            else
            {
                ViewData["Message"] = msg;
            }
            return View();
        }
        public IActionResult FilterCategories([FromBody] CategoryFilterDto criteria, [FromQuery] int page)
        {
            Expression<Func<Category, bool>> filterExpression = category =>
                (string.IsNullOrEmpty(criteria.Name) || category.Name.Contains(criteria.Name)) &&
                (string.IsNullOrEmpty(criteria.Description) || category.Description.Contains(criteria.Description)) &&
                (string.IsNullOrEmpty(criteria.Id) || category.ID.Equals(Int32.Parse(criteria.Id))) &&
                (string.IsNullOrEmpty(criteria.Status) || (criteria.Status == "Active"
            ? category.Status == Enums.DataStatus.Inserted || category.Status == Enums.DataStatus.Updated
            : category.Status == Enums.DataStatus.Deleted));

            var filteredCategories = _repoCategory.GetByFilter(filterExpression).ToPagedList(page, criteria.ItemsPerPage);

            var jsonResponse = new
            {
                PageNumber = filteredCategories.PageNumber, // Mevcut sayfa
                PageCount = filteredCategories.PageCount,   // Toplam sayfa
                HasNextPage = filteredCategories.HasNextPage, // Sonraki sayfa var mı
                HasPreviousPage = filteredCategories.HasPreviousPage, // Önceki sayfa var mı
                Categories = filteredCategories // Sayfa içerikleri
            };

            var serializedResponse = JsonConvert.SerializeObject(jsonResponse, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            });

            return Content(serializedResponse, "application/json");
        }
        public IActionResult Update(int id)
        {
            Category category = _repoCategory.GetById(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            string msg = "Kayıt güncelleme BAŞARISIZ!";
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _repoCategory.Update(category);
            Category ControlObject = _repoCategory.GetById(category.ID);
            if (category.Name == ControlObject.Name && category.Description == ControlObject.Description)
            {
                msg = "Kayıt başarıyla güncellendi! (" + category.Name + ")";
                ViewData["Message"] = msg;
            }
            else
            {
                ViewData["Message"] = msg;
            }
            return View();
        }
        public IActionResult Delete(int id) 
        {
            _repoCategory.Delete(id);
            return RedirectToAction("Index");
        }
    }
    
}
