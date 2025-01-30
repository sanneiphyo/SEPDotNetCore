using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.Project1.MvcApp.Models;
using SEPDotNetCore.Shared;

namespace SEPDotNetCore.Project1.MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClientService _httpClientService;

        public HomeController(ILogger<HomeController> logger, HttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;
        }

        public async Task<IActionResult> Index()
        {
            var lst = await _httpClientService.SendAsync<List<BlogModel>>("api/blog", EnumHttpMethod.Get);
            return View(lst);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

      
    }

    public class BlogModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
