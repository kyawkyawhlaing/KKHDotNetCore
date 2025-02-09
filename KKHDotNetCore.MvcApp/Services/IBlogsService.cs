using KKHDotNetCore.MvcApp.Models;
using KKHDotNetCore.MvcApp.Models.RequestModels;
using KKHDotNetCore.MvcApp.Models.ViewModels;


namespace KKHDotNetCore.MvcApp.Services
{
    public interface IBlogsService
    {
        public Result<BlogViewModel> CreateBlog(BlogRequestModel requestModel);
        public Result<BlogViewModel> GetBlog(int blogId);
        public Result<List<BlogViewModel>> GetBlogs();
        public Result<BlogViewModel> UpdateBlog(int id, BlogRequestModel requestModel);
        public Result<BlogViewModel> DeleteBlog(int id);

    }
}