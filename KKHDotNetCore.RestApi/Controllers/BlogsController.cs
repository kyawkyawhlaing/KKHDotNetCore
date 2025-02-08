using KKHDotNetCore.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace KKHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BlogsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _db.TblBlogs.AsNoTracking().ToList();  
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges(true);
            return Ok(item);
        }

    }
}
