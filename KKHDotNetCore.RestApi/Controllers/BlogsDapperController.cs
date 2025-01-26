using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using KKHDotNetCore.RestApi.Models.ViewModels;

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
    }
}
