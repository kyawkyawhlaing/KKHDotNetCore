using KKHDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KKHDotNetCore.MvcApp.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Execute<T>(Result<T> model)
        {
            if(model.IsValidationError)
            {
                return BadRequest(model);
            }
            if(model.IsSystemError)
            {
                return StatusCode(500, model);
            }
            return Ok(model);
        }

    }
}
