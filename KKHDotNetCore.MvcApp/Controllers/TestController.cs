using KKHDotNetCore.MvcApp.Models.RequestModels;
using KKHDotNetCore.MvcApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKHDotNetCore.MvcApp.Controllers
{
    public class TestController : Controller
    {
        private readonly IBlogsService _blogService;

        public TestController(IBlogsService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [ActionName("Create")]
        public IActionResult CreateBlog()
        {
            return View("CreateBlog");
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateBlog(BlogRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateBlog", requestModel);
            }
            try
            {
                _blogService.CreateBlog(requestModel);
                TempData["IsSuccess"] = true;
                TempData["Message"] = "New Blog is created successfully";
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "New Blog is failed to create";
            }
            return RedirectToAction("Index");
        }
    }
}
