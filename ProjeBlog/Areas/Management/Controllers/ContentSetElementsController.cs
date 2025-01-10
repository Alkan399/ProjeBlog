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
    public class ContentSetElementsController : Controller
    {
        MyDbContext _db;
        IRepository<ContentSetElement> _repoContentSetElement;
        public ContentSetElementsController(MyDbContext db, IRepository<ContentSetElement> repoContentSetElement)
        {
            _db = db;
            _repoContentSetElement = repoContentSetElement;
        }
        public IActionResult Index()
        {
            List<ContentSetElement> contentSetElements = _repoContentSetElement.GetAll();
            return View(contentSetElements);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContentSetElement contentSetElement) 
        {
            if (!ModelState.IsValid)
            {
                return View(contentSetElement);
            }
            _repoContentSetElement.Add(contentSetElement);
            return View();
        }
    }
}
