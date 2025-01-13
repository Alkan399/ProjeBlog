using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class ContentSetElementsController : Controller
    {
        MyDbContext _db;
        IContentSetElementRepository _repoContentSetElement;
        public ContentSetElementsController(MyDbContext db, IContentSetElementRepository repoContentSetElement)
        {
            _db = db;
            _repoContentSetElement = repoContentSetElement;
        }
        public IActionResult Index()
        {
            List<ContentSetElement> contentSetElements = _repoContentSetElement.GetContentSetElementsWithContentSets();
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
            contentSetElement.Recent = contentSetElement.ContentSetOption == "Recent";
            contentSetElement.MostPopular = contentSetElement.ContentSetOption == "MostPopular";
            contentSetElement.Custom = contentSetElement.ContentSetOption == "Custom";
            _repoContentSetElement.Add(contentSetElement);
            return View();
        }
        public IActionResult Update(int id)
        {
            ContentSetElement contentSetElement = _repoContentSetElement.GetContentSetElementWithcontentSetById(id);
            return View(contentSetElement);
        }
        [HttpPost]
        public IActionResult Update(ContentSetElement contentSetElement)
        {
            if (!ModelState.IsValid)
            {
                return View(contentSetElement);
            }
            contentSetElement.Recent = contentSetElement.ContentSetOption == "Recent";
            contentSetElement.MostPopular = contentSetElement.ContentSetOption == "MostPopular";
            contentSetElement.Custom = contentSetElement.ContentSetOption == "Custom";
            _repoContentSetElement.Update(contentSetElement);
            return View(contentSetElement);
        }
        public IActionResult Delete(int id) 
        {
            _repoContentSetElement.SpecialDelete(id);
            return RedirectToAction("Index");
        }
    }
}
