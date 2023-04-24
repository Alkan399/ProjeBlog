using Microsoft.AspNetCore.Mvc;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    public class BasvuruController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
