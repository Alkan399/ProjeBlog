using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.Models.Dto;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using X.PagedList.Extensions;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class UserController : Controller
    {
        MyDbContext _db;
        IAppUserRepository _repoAppUser;
        IRepository<AppUserDetail> _repoAppUserDetail;
        IRepository<Basvuru> _repoBasvuru;

        public UserController(MyDbContext db,
            IAppUserRepository repoAppUser,
            IRepository<AppUserDetail> repoAppUserDetail,
            IRepository<Basvuru> repoBasvuru)
        {
            _db = db;
            _repoAppUser = repoAppUser;
            _repoAppUserDetail = repoAppUserDetail;
            _repoBasvuru = repoBasvuru;
        }
        public IActionResult Index()
        {
            AppUser user = _repoAppUser.GetUserWithDetailsByCookie(HttpContext);
            return View(user);
        }
        public IActionResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PasswordChange(PasswordChangeViewModel passwordChange)
        {
            string msg = "Şifre güncelleme BAŞARISIZ! Lütfen bilgileri doğru girdiğinizden emin olun.";
            int Id = _repoAppUser.GetUserId(HttpContext);
            AppUser selectedUser = _repoAppUser.Default(x => x.ID == Id && x.Status != Enums.DataStatus.Deleted);
            bool isValid = BCrypt.Net.BCrypt.Verify(passwordChange.OldPassword, selectedUser.Password);
            if (!ModelState.IsValid)
            {
                return View(passwordChange);
            }
            selectedUser.Password = BCrypt.Net.BCrypt.HashPassword(passwordChange.NewPassword);
            _repoAppUser.Update(selectedUser);

            msg = "Şifre güncelleme BAŞARILI!";
            ViewData["Message"] = msg;
            return View();
        }
        public IActionResult GetCurrentUser()
        {
            return View();
        }
        public IActionResult UserList()
        {
            List<AppUser> users = _repoAppUser.GetUsersWithDetail();
            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AppUser user)
        {
            string msg = "Kayıt oluşturma  BAŞARISIZ!";

            if (!ModelState.IsValid) 
            {
                return View(user);
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _repoAppUser.Add(user);

            AppUser c = _repoAppUser.GetById(user.ID);
            if (user.UserName == c.UserName && user.Role == c.Role && user.Email == c.Email)
            {
                msg = "Kayıt oluşturma BAŞARILI!";
                ViewData["Message"] = msg;
            }
            else
            {
                ViewData["Message"] = msg;
            }
            return View();
        }
        public IActionResult Update(int id)
        {
            AppUser appUser = _repoAppUser.GetUserWithDetailById(id);
            return View(appUser);
        }
        [HttpPost]
        public IActionResult Update(AppUser appUser)
        {
            if (!ModelState.IsValid)
            {
                return View(appUser);
            }

            string msg = "Kayıt güncelleme BAŞARISIZ!";
            _repoAppUser.UpdateUserWithDetails(appUser);
            AppUser c = _repoAppUser.GetById(appUser.ID);
            if (appUser.UserName == c.UserName && appUser.Role == c.Role && appUser.Email == c.Email && appUser.AppUserDetail.DateOfBirth == c.AppUserDetail.DateOfBirth && appUser.AppUserDetail.FirstName == c.AppUserDetail.FirstName && appUser.AppUserDetail.LastName == c.AppUserDetail.LastName)
            {
                msg = "Kayıt güncelleme BAŞARILI!";
                ViewData["Message"] = msg;
            }
            else
            {
                ViewData["Message"] = msg;
            }
            return View(appUser);
        }
        public IActionResult UpdateDetails(AppUserDetail appUserDetail, int detailId)
        {
            string msg = "Kayıt güncelleme BAŞARISIZ!";
            AppUserDetail UserDetail = _repoAppUserDetail.GetById(detailId);
            UserDetail.FirstName = appUserDetail.FirstName;
            UserDetail.LastName = appUserDetail.LastName;
            UserDetail.DateOfBirth = appUserDetail.DateOfBirth;
            UserDetail.PhoneNumber = appUserDetail.PhoneNumber;
            UserDetail.ProfilePicture = appUserDetail.ProfilePicture;
            _repoAppUserDetail.Update(UserDetail);
            AppUserDetail c = _repoAppUserDetail.GetById(detailId);
            if (UserDetail.FirstName == c.FirstName && UserDetail.LastName == c.LastName && UserDetail.DateOfBirth == c.DateOfBirth && UserDetail.PhoneNumber == c.PhoneNumber && UserDetail.ProfilePicture == c.ProfilePicture)
            {
                msg = "Kayıt güncelleme BAŞARILI!";
                TempData["Message"] = msg;
            }
            else
            {
                TempData["Message"] = msg;
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            _repoAppUser.Delete(id);
            return RedirectToAction("UserList");
        }
        public IActionResult CreateFromBasvuru()
        {
            List<Basvuru> Basvurus =  _repoBasvuru.GetAll();
            return View(Basvurus);
        }
        public IActionResult FilterUsers([FromBody] AppUserFilterDto criteria, [FromQuery]int page)
        {
            Expression<Func<AppUser, bool>> filterExpression = user =>
                (string.IsNullOrEmpty(criteria.UserName) || user.UserName.Contains(criteria.UserName)) &&
                (string.IsNullOrEmpty(criteria.Email) || user.Email.Contains(criteria.Email)) &&
                (string.IsNullOrEmpty(criteria.Role) || (criteria.Role == "Admin"
            ? user.Role == Enums.Role.Admin : user.Role == Enums.Role.User)) &&
                (string.IsNullOrEmpty(criteria.Status) || (criteria.Status == "Active"
            ? user.Status == Enums.DataStatus.Inserted || user.Status == Enums.DataStatus.Updated
            : user.Status == Enums.DataStatus.Deleted));

            var filteredUsers = _repoAppUser.GetUserWithDetailsByFilter(filterExpression).ToPagedList(page, criteria.ItemsPerPage);
            
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
