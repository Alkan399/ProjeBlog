using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjeBlog.Models.apiModels;
using ProjeBlog.RepositoryPattern;

namespace ProjeBlog.Areas.Blog.Controllers.ApiControllers
{
    [ApiController]
    [Area("Blog")]
    [Route("api/Blog/ApiControllers/UtilityMethods")]
    public class UtilityMethodsApiController : Controller
    {
        UtilityMethods _Utilities;
        public UtilityMethodsApiController(UtilityMethods Utilities)
        {
            _Utilities = Utilities;
        }

        [HttpPost("GetContentSetElements")]
        public IActionResult GetContentSetElements([FromBody]LocationRequest locationRequest)
        {
            Enums.ElementLocation elementLocation = (Enums.ElementLocation)locationRequest.Location;
            var result = _Utilities.ContentSetElements(elementLocation);
            var serializedResponse = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            });
            return Ok(serializedResponse);
        }
    }
}
