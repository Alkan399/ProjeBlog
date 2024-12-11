using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models;
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
        public BasvuruController(MyDbContext db)
        {
            _db = db;
        }
        public IActionResult BasvuruList()
        {
            List<Basvuru> content = _db.Basvurus.ToList();
            return View(content);
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
