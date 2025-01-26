using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKHDotNetCore.ConsoleApp
{
    public class EFcoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.AsNoTracking().ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
            }
        }
    
        public void Create(string title, string author, string content)
        {
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(new Models.BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
                DeleteFlag = false,
            });
            int result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Adding success" : "Adding failed");
        }
    }
}
