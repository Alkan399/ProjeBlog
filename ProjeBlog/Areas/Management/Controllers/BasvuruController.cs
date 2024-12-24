using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Mozilla;
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
        public IActionResult Index()
        {
            return View();
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
            if (!ModelState.IsValid)
            {
                return View(basvuru);
            }

            _basvuruRepository.Add(basvuru);
            return View();
        }
        public IActionResult Update(int id)
        {
            Basvuru basvuru = _basvuruRepository.GetById(id);
            return View(basvuru);
        }
        [HttpPost]
        public IActionResult Update(Basvuru basvuru)
        {
            if (!ModelState.IsValid)
            {
                return View(basvuru);
            }

            string msg = "Kayıt güncelleme BAŞARISIZ!";
            _basvuruRepository.Update(basvuru);
            if (basvuru == _basvuruRepository.GetById(basvuru.ID)) 
            {
                msg = "Kayıt güncelleme BAŞARILI!";

            }
            ViewData["Message"] = msg;
            return View();
        }
        public IActionResult Delete(int id)
        {
            _basvuruRepository.Delete(id);
            return RedirectToAction("BasvuruList");
        }
        public IActionResult FilterBasvurus([FromBody] BasvuruFilterDto criteria, [FromQuery] int page)
        {
            Expression<Func<Basvuru, bool>> filterExpression = basvuru =>
                (string.IsNullOrEmpty(criteria.FirstName) || basvuru.FirstName.Contains(criteria.FirstName)) &&
                (string.IsNullOrEmpty(criteria.Email) || basvuru.Email.Contains(criteria.Email)) &&
                (string.IsNullOrEmpty(criteria.LastName) || basvuru.LastName.Contains(criteria.LastName)) &&
                (string.IsNullOrEmpty(criteria.DateOfBirth) || basvuru.DateOfBirth.Date.ToString() == criteria.DateOfBirth) &&
                (string.IsNullOrEmpty(criteria.Status) || (criteria.Status == "Active"
            ? basvuru.Status == Enums.DataStatus.Inserted || basvuru.Status == Enums.DataStatus.Updated
            : basvuru.Status == Enums.DataStatus.Deleted));

            var filteredBasvurus = _basvuruRepository.GetByFilter(filterExpression).ToPagedList(page, criteria.ItemsPerPage);

            var jsonResponse = new
            {
                PageNumber = filteredBasvurus.PageNumber, // Mevcut sayfa
                PageCount = filteredBasvurus.PageCount,   // Toplam sayfa
                HasNextPage = filteredBasvurus.HasNextPage, // Sonraki sayfa var mı
                HasPreviousPage = filteredBasvurus.HasPreviousPage, // Önceki sayfa var mı
                Contents = filteredBasvurus // Sayfa içerikleri
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
