using KKHDotNetCore.Database.Models;
using KKHDotNetCore.MvcApp.Models;
using KKHDotNetCore.MvcApp.Models.RequestModels;
using KKHDotNetCore.MvcApp.Models.ResponseModels;
using KKHDotNetCore.MvcApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KKHDotNetCore.MvcApp.Services
{
    public class BlogsService : IBlogsService
    {
        private readonly AppDbContext _db;

        public BlogsService(AppDbContext db)
        {
            _db = db;
        }

        public Result<BlogViewModel> CreateBlog(BlogRequestModel requestModel)
        {
            Result<BlogViewModel> model = new Result<BlogViewModel>();
            try
            {
                _db.TblBlogs.Add(new TblBlog
                {
                    BlogTitle   = requestModel.BlogTitle!,
                    BlogAuthor  = requestModel.BlogAuthor!,
                    BlogContent = requestModel.BlogContent!
                });
                _db.SaveChanges();
                model = Result<BlogViewModel>.Success(null);
                goto Result;
            }
            catch (Exception ex)
            {
                model = Result<BlogViewModel>.SystemError(ex.Message);
                goto Result;
            }
            Result:
                return model;
        }

        public Result<BlogViewModel> GetBlog(int blogId)
        {
            Result<BlogViewModel> model = new Result<BlogViewModel>();
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == blogId);
            if (item is null)
            {
                model = Result<BlogViewModel>.ValidationError("Blog not found");
                goto Result;
            }
            else
            {
                model = Result<BlogViewModel>.Success(new BlogViewModel()
                {
                    BlogId = item.BlogId,
                    BlogTitle = item.BlogTitle,
                    BlogAuthor = item.BlogAuthor,
                    BlogContent = item.BlogContent
                });
                goto Result;
            }
            Result:
                return model;
        }

        public Result<List<BlogViewModel>> GetBlogs()
        {
            Result<List<BlogViewModel>> model = new Result<List<BlogViewModel>>();
            var items = _db.TblBlogs.AsNoTracking().Select(x => new BlogViewModel
            {
                BlogId      = x.BlogId,
                BlogTitle   = x.BlogTitle,
                BlogAuthor  = x.BlogAuthor,
                BlogContent = x.BlogContent
            }).ToList();
            if(items is not null)
            {
                model = Result<List<BlogViewModel>>.Success(items); 
                goto Result;
            }
            else
            {
                model = Result<List<BlogViewModel>>.NotFound("Blogs not found");
                goto Result;
            }
            Result:
                return model;
        }


        public Result<BlogViewModel> UpdateBlog(int id, BlogRequestModel requestModel)
        {
            Result<BlogViewModel> model = new Result<BlogViewModel>();
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id)!;
            if (requestModel.BlogTitle is not null)
            {
                item.BlogTitle = requestModel.BlogTitle;
            }
            if (requestModel.BlogAuthor is not null)
            {
                item.BlogAuthor = requestModel.BlogAuthor;
            }
            if (requestModel.BlogContent is not null)
            {
                item.BlogContent = requestModel.BlogContent;
            }
            _db.Entry(item).State = EntityState.Modified;
            int result = _db.SaveChanges();
            if(result is 1)
            {
                model = Result<BlogViewModel>.Success(null, "Blog has updated successfully");
                goto Result;
            }
            else
            {
                model = Result<BlogViewModel>.SystemError("Blog has Failed to Update");
                goto Result;
            }
            Result:
                return model;

        }

        public Result<BlogViewModel> DeleteBlog(int id)
        {
            Result<BlogViewModel> model = new Result<BlogViewModel>();
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id)!;
            _db.Entry(item).State = EntityState.Deleted;
            int result = _db.SaveChanges();
            if (result is 1)
            {
                model = Result<BlogViewModel>.Success(null, "Blog has deleted successfully");
                goto Result;
            }
            else
            {
                model = Result<BlogViewModel>.SystemError("Blog has Fail to delete");
                goto Result;
            }
            Result:
                return model;
        }
    }
}
