using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Collections.Generic;

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

    }
}
