using KKHDotNetCore.Database.Models;
using KKHDotNetCore.MvcApp.Models.RequestModels;
using KKHDotNetCore.MvcApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KKHDotNetCore.MvcApp.Services
{
    public interface IBlogsService
    {
        public void CreateBlog(BlogRequestModel requestModel);
        public BlogViewModel GetBlog(int blogId);
        public List<BlogViewModel> GetBlogs();
        public void UpdateBlog(int id, BlogRequestModel requestModel);
        public void DeleteBlog(int id);

    }
}