 using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Models;
using ProjeBlog.Models.LogModels;
using ProjeBlog.RepositoryPattern.Base;
using ProjeBlog.RepositoryPattern.Concrete;
using ProjeBlog.RepositoryPattern.Interfaces;
using ProjeBlog.Services;
using System;
using System.Collections.Generic;
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
        IRepository<VisitorsLog> _visitorLogRepository;
        IStatisticsService<Content> _contentStatisticsService;

        List<Object> listRepos = new List<object>();
        

        public HomeController(IContentRepository contentRepository, IAppUserRepository appUserRepository, IRepository<Comment> commentRepository, IRepository<VisitorsLog> visitorLogRepository, IStatisticsService<Content> contentStatisticsService)
        {
            _contentRepository = contentRepository;
            _appUserRepository = appUserRepository;
            _commentRepository = commentRepository;
            _visitorLogRepository = visitorLogRepository;

            _contentStatisticsService = contentStatisticsService;

            listRepos.Add(_contentRepository);
            listRepos.Add(_appUserRepository);
            listRepos.Add(_commentRepository);
            listRepos.Add(_visitorLogRepository);
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
            int uniqueVisitorsToday = _visitorLogRepository.GetActives().Where(x => x.CreatedDate.Date == System.DateTime.Today.Date).GroupBy(y => y.ip).Count();

            ViewBag.ContentViewsToday = contentViewsToday;
            ViewBag.ContentCountToday = contentsCreatedToday;
            ViewBag.UsersCreatedToday = usersCreatedToday;
            ViewBag.CommentsCreatedToday = commentsCreatedToday;
            ViewBag.UniqueVisitorsToday = uniqueVisitorsToday;
            return View();
        }
        public IActionResult StatisticsIndex(string entityName)
        {
            foreach (var repo in listRepos)
            {
                Console.WriteLine(repo.GetType());
            }
            return View();
        }
    }
}
