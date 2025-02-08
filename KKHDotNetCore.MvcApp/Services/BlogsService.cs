using KKHDotNetCore.Database.Models;
using KKHDotNetCore.MvcApp.Models.RequestModels;
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

        public void CreateBlog(BlogRequestModel requestModel)
        {
            _db.TblBlogs.Add(new TblBlog
            {
                BlogTitle   = requestModel.BlogTitle!,
                BlogAuthor  = requestModel.BlogAuthor!,
                BlogContent = requestModel.BlogContent!
            });
            _db.SaveChanges();
        }

        public BlogViewModel GetBlog(int blogId)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == blogId);
            if (item is null)
            {
                return new BlogViewModel() {};
            }
            return new BlogViewModel()
            {
                BlogId      = item.BlogId,
                BlogTitle   = item.BlogTitle,
                BlogAuthor  = item.BlogAuthor,
                BlogContent = item.BlogContent
            };
        }

        public List<BlogViewModel> GetBlogs()
        {
            var items = _db.TblBlogs.AsNoTracking().Select(x => new BlogViewModel
            {
                BlogId      = x.BlogId,
                BlogTitle   = x.BlogTitle,
                BlogAuthor  = x.BlogAuthor,
                BlogContent = x.BlogContent
            }).ToList();
            if (items is null)
            {
                Console.WriteLine("Item is null");
            }
            return items!;
        }


        public void UpdateBlog(int id, BlogRequestModel requestModel)
        {
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
            _db.SaveChanges();

        }

        public void DeleteBlog(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id)!;
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
        }
    }
}
