using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Areas.Blog.Controllers;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class BasvuruController : Controller
    {
        MyDbContext _db;
        int id;
        IRepository<Basvuru> _basvuruRepository;
        public BasvuruController(MyDbContext db, IRepository<Basvuru> basvuruRepository)
        {
            _db = db;
            _basvuruRepository = basvuruRepository;
        }
        public IActionResult BasvuruList()
        {
            List<Basvuru> content = _db.Basvurus.ToList();
            return View(content);
        }
        public IActionResult Create(Basvuru basvuru)
        {
            _basvuruRepository.Add(basvuru);
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
