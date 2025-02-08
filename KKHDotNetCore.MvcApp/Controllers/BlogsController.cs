
using KKHDotNetCore.MvcApp.Models.RequestModels;
using KKHDotNetCore.MvcApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KKHDotNetCore.MvcApp.Controllers
{
    public class BlogsController : Controller
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
            //if (!ModelState.IsValid)
            //{
            //    return View("BlogCreate", requestModel);
            //}
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

        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            return View("BlogEdit", _blogService.GetBlog(id));
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogRequestModel requestModel)
        {
            try
            {
                _blogService.UpdateBlog(id, requestModel);
                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog is updated successfully";
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Updating failed";
            }

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public IActionResult BlogDelete(int id)
        {
            try
            {
                _blogService.DeleteBlog(id);
                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog is removed successfully";
            }
            catch(Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "Deleting failed";
            }

            return RedirectToAction("Index");
        }
    }
}
