using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.Domain.Features.Blog;

namespace SEPdotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var lst = _blogService.GetBlogs();
            return View(lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");      
        }
    }
}
