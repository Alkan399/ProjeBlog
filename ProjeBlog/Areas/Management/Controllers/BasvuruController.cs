using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjeBlog.Areas.Blog.Controllers;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Basvuru basvuru)
        {
            _basvuruRepository.Add(basvuru);
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FilterBasvurus([FromBody] BasvuruFilterDto criteria, [FromQuery] int page)
        {
            Expression<Func<Basvuru, bool>> filterExpression = basvuru =>
                (string.IsNullOrEmpty(criteria.FirstName) || basvuru.FirstName.Contains(criteria.FirstName)) &&
                (string.IsNullOrEmpty(criteria.Email) || basvuru.Email.Contains(criteria.Email)) &&
                (string.IsNullOrEmpty(criteria.LastName) || basvuru.LastName.Contains(criteria.LastName)) &&
                (string.IsNullOrEmpty(criteria.Status) || (criteria.Status == "Active"
            ? basvuru.Status == Enums.DataStatus.Inserted || basvuru.Status == Enums.DataStatus.Updated
            : basvuru.Status == Enums.DataStatus.Deleted));

            var filteredUsers = _basvuruRepository.GetByFilter(filterExpression).ToPagedList(page, criteria.ItemsPerPage);

            var jsonResponse = new
            {
                PageNumber = filteredUsers.PageNumber, // Mevcut sayfa
                PageCount = filteredUsers.PageCount,   // Toplam sayfa
                HasNextPage = filteredUsers.HasNextPage, // Sonraki sayfa var mı
                HasPreviousPage = filteredUsers.HasPreviousPage, // Önceki sayfa var mı
                Contents = filteredUsers // Sayfa içerikleri
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
