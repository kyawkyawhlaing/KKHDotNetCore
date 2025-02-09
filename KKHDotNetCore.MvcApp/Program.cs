using KKHDotNetCore.Database.Models;
using KKHDotNetCore.MvcApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// use keyedservice when one interface has multiple services and to avoid ambiguous error(similar to @Qualifier in Java Spring Framework)
builder.Services.AddKeyedScoped<ITestKeyedService, TestKeyedService1>("service1");
builder.Services.AddKeyedScoped<ITestKeyedService, TestKeyedService2>("service2");
builder.Services.AddScoped<IBlogsService, BlogsService>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
