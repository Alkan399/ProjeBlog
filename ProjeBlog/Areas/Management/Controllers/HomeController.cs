 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Concrete;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Linq;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class HomeController : Controller
    {
        IContentRepository _contentRepository;
        IAppUserRepository _appUserRepository;
        IRepository<Comment> _commentRepository;
        public HomeController(IContentRepository contentRepository, IAppUserRepository appUserRepository, IRepository<Comment> commentRepository)
        {
            _contentRepository = contentRepository;
            _appUserRepository = appUserRepository;
            _commentRepository = commentRepository;

        }
        public IActionResult Index()
        {
            int contentsCreatedToday = _contentRepository.GetActives().Where(x => x.CreatedDate.Date == System.DateTime.Today.Date).Count();
            int usersCreatedToday = _appUserRepository.GetActives().Where(x => x.CreatedDate.Date == System.DateTime.Today.Date).Count();
            int commentsCreatedToday = _commentRepository.GetActives().Where(x => x.CreatedDate.Date == System.DateTime.Today.Date).Count();
            int contentViewsToday = _contentRepository.GetContentWithUserAndStatistics(
                    x => x.Status != Enums.DataStatus.Deleted &&
                         x.ContentStatistics.Any(cs => cs.Date.Date == DateTime.Now.Date)
                )
                .Select(x => x.ContentStatistics
                    .Where(cs => cs.Date.Date == DateTime.Now.Date)
                    .Sum(cs => cs.ViewCount)
                )
                .Sum();

            ViewBag.ContentViewsToday = contentViewsToday;
            ViewBag.ContentCountToday = contentsCreatedToday;
            ViewBag.UsersCreatedToday = usersCreatedToday;
            ViewBag.CommentsCreatedToday = commentsCreatedToday;
            return View();
        }
    }
}
