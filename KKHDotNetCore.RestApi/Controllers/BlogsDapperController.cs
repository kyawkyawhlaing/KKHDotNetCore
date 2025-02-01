using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using KKHDotNetCore.RestApi.Models.ViewModels;
using KKHDotNetCore.Database.Models;

namespace KKHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=KKHDotNetCore;User ID=sa;Password=sa";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst;
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag=0;";
                lst = db.Query<Models.ViewModels.BlogViewModel>(query).ToList();
            }
            return Ok(lst);  
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"INSERT INTO dbo.Tbl_Blog(
                                BlogTitle,
                                BlogAuthor,
                                BlogContent,
                                DeleteFlag
                             )
                            VALUES(
                                @BlogTitle,
                                @BlogAuthor,
                                @BlogContent,
                                0
                            )";
                int result = db.Execute(query, new
                {
                    BlogTitle = blog.BlogTitle,
                    BlogAuthor = blog.BlogAuthor,
                    BlogContent = blog.BlogContent
                });
                return Created();
            }
        }

        [HttpPut]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"Select * From [dbo].[Tbl_Blog] Where DeleteFlag=0 And BlogId=@BlogId";
                var item = db.Query<Models.DataModels.BlogDataModel>(query, new Models.DataModels.BlogDataModel
                {
                    BlogId = id,
                }).FirstOrDefault();

                if (item is null)
                {
                    Console.WriteLine("No Data Found");
                }
                else
                {
                    string insertQuery = $@"Update [dbo].[Tbl_Blog] Set
                        BlogTitle   = @BlogTitle,
                        BlogAuthor  = @BlogAuthor,
                        BlogContent = @BlogContent,
                        DeleteFlag  = @DeleteFlag
                    Where BlogId=@BlogId
                   
                    ";
                    int result = db.Execute(insertQuery, new
                    {
                        BlogId      = id,
                        BlogTitle   = blog.BlogTitle,
                        BlogAuthor  = blog.BlogAuthor,
                        BlogContent = blog.BlogContent,
                        DeleteFlag  = blog.DeleteFlag,
                    });
                    Console.WriteLine(result == 1 ? "Edited successfully" : "Edited failed");
                }
            }
                return Ok(blog);
        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"Delete From dbo.Tbl_Blog Where BlogId=@BlogId";
                int result = db.Execute(query, new { BlogId = id });
            }
            return Ok();
        }
    }
}
