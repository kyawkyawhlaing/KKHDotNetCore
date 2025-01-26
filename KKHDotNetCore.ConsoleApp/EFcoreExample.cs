using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public void Edit(int id )
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if ( item is null )
            {
                Console.WriteLine("Data not found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine(item.DeleteFlag);

        }
    }

    public void Update(string title, string author, string content)
    {
        AppDbContext db = new AppDbContext();
        var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            Console.WriteLine("Data not found");
            return;
        }
        if (string.IsNullOrEmpty(title))
        {
            item.BlogTitle = title;
        }
        if (string.IsNullOrEmpty(author))
        {
            item.BlogAuthor = author;
        }
        if (string.IsNullOrEmpty(content))
        {
            item.BlogContent = content;
        }
        db.Entry(item).State = EntityState.Modified;
        var result = db.SaveChanges();
        Console.WriteLine(result == 1 ? "Updating success" : "Updating failed");
    }
}
