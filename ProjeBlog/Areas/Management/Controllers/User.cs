using Microsoft.AspNetCore.Mvc;

namespace ProjeBlog.Areas.Management.Controllers
{
    [Area("Management")]
    public class User : Controller
    {
        public IActionResult UserList()
        {
            return View();
        }
    }
}
