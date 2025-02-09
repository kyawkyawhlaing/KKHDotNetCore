using KKHDotNetCore.MvcApp.Models;
using KKHDotNetCore.MvcApp.Models.ResponseModels;
using KKHDotNetCore.MvcApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KKHDotNetCore.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITestKeyedService _service;

        public HomeController(ILogger<HomeController> logger, [FromKeyedServices("service2")]ITestKeyedService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Hello from ViewBag"; 
            HomeResponseModel model = new HomeResponseModel();
            model.Message = "Hello from Model";
            ViewBag.Service = _service.Print();
            return View(model);
        }
    }
}
