using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.Models.Dto;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class UserController : Controller
    {
        MyDbContext _db;
        IAppUserRepository _repoAppUser;

        public UserController(MyDbContext db,
            IAppUserRepository repoAppUser)
        {
            _db = db;
            _repoAppUser = repoAppUser;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserList()
        {
            List<AppUser> users = _repoAppUser.GetUsersWithDetail(
                );
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AppUser user)
        {
            
            _repoAppUser.Add(user);
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
            _repoAppUser.UpdateUserWithDetails(appUser);
            return RedirectToAction("UserList");   
        }
        public IActionResult Delete(int id)
        {
            _repoAppUser.Delete(id);
            return RedirectToAction("UserList");
        }
        [Route("User/FilterUsers")]
        [HttpPost]
        public IActionResult FilterUsers([FromBody] AppUserFilterDto criteria)
        {
            Expression<Func<AppUser, bool>> filterExpression = user =>
                (string.IsNullOrEmpty(criteria.UserName) || user.UserName.Contains(criteria.UserName)) &&
                (string.IsNullOrEmpty(criteria.Email) || user.Email.Contains(criteria.Email)) &&
                (string.IsNullOrEmpty(criteria.Role) || (criteria.Role == "Admin"
            ? user.Role == Enums.Role.Admin : user.Role == Enums.Role.User)) &&
                (string.IsNullOrEmpty(criteria.Status) || (criteria.Status == "Active"
            ? user.Status == Enums.DataStatus.Inserted || user.Status == Enums.DataStatus.Updated
            : user.Status == Enums.DataStatus.Deleted));

            var filteredUsers = _repoAppUser.GetUserWithDetailsByFilter(filterExpression);
            if (filteredUsers == null || !filteredUsers.Any())
            {
                return Json(new { message = "No users found." });
            }
            return Json(filteredUsers);
        }
    }
}
