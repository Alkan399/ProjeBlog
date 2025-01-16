using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Context;
using ProjeBlog.Models.ConstantModels;
using ProjeBlog.RepositoryPattern.Interfaces;
using System.Linq;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class ConstantsController : Controller
    {
        IRepository<About> _repoAbout;
        MyDbContext _db;
        public ConstantsController(IRepository<About> repoAbout, MyDbContext db)
        {
            _db = db;
            _repoAbout = repoAbout;
        }
        public IActionResult IndexAbout()
        {
            About about = _repoAbout.GetActives().FirstOrDefault();
            return View(about);
        }
        public IActionResult UpdateAbout()
        {
            About about = _repoAbout.GetActives().FirstOrDefault();
            if (about == null)
            {
                about = new About();
            }
            
            return View(about);
        }
        [HttpPost]
        public IActionResult UpdateAbout(About about)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }
            _repoAbout.Update(about);
            return View(about);
        }

    }
}
