
using KKHDotNetCore.MvcApp.Models.RequestModels;
using KKHDotNetCore.MvcApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKHDotNetCore.MvcApp.Controllers
{
    public class BlogsController : BaseController
    {
        private readonly IBlogsService _blogService;

        public BlogsController(IBlogsService blogService)
        {
            _blogService = blogService;
        }

        [ActionName("Index")]
        public IActionResult BlogList()
        {
            var blogLst = _blogService.GetBlogs();
            return View("BlogList", blogLst);
        }

        [HttpGet]
        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult BlogCreate(BlogRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return View("BlogCreate", requestModel);
            }
            _blogService.CreateBlog(requestModel);
            TempData["IsInitial"] = false;

            return RedirectToAction("Index");
        }

        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            return View("BlogEdit", _blogService.GetBlog(id));
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogRequestModel requestModel)
        {

            _blogService.UpdateBlog(id, requestModel);
            TempData["IsInitial"] = false;

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public IActionResult BlogDelete(int id)
        {

            _blogService.DeleteBlog(id);
            TempData["IsInitial"] = false;

            return RedirectToAction("Index");
        }
    }
}
