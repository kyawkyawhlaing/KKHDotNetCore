using Dapper;
using KKHDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKHDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=KKHDotNetCore;User ID=sa;Password=sa";

        public void Read()
        {
            using(IDbConnection db = new SqlConnection( _connectionString ))
            {
                string query = "select * from tbl_blog where DeleteFlag=0;";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach ( var item in lst )
                {
                    Console.WriteLine( item.BlogId );
                    Console.WriteLine( item.BlogTitle );
                    Console.WriteLine( item.BlogAuthor );
                    Console.WriteLine( item.BlogContent );
                }
            }
        }

        public void Create(string title, string author, string content)
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
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Saving successful" : "Saving failed" ); 
            }
        }
    
        public void Edit(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString)) 
            {
                string query = $@"Select * From [dbo].[Tbl_Blog] Where DeleteFlag=0 And BlogId=@BlogId";
                var item = db.Query<BlogDataModel>(query, new BlogDataModel 
                { 
                    BlogId = id,
                }).FirstOrDefault();

                if (item is null)
                {
                    Console.WriteLine("No Data Found");
                    return;
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
                        BlogId = id,
                        BlogTitle = item.BlogTitle,
                        BlogAuthor = item.BlogAuthor,
                        BlogContent = "new content updated",
                        DeleteFlag = true,
                    });
                    Console.WriteLine(result == 1 ? "Edited successfully": "Edited failed");
                }
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine(item.BlogAuthor);
            }

        }
    
        public void Delete(int id)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"Delete From dbo.Tbl_Blog Where BlogId=@BlogId";
                int result = db.Execute(query, new { BlogId = id });
                Console.WriteLine(result == 1 ? "Deleted successfully" : "Deleted Failed");
            }
        }
    }
}
