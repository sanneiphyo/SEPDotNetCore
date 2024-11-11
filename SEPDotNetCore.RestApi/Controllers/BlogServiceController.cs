using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.Database.Models;
using SEPDotNetCore.Domain.Features.Blog;

namespace SEPDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        private readonly BlogServiceController _service;

        public BlogServiceController()
        {
            _service = new BlogServiceController();
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lst = _service.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _service.GetBlog(id);
         
            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            var model = _service.CreateBlog(blog);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var item = _service.UpdateBlog(id, blog);

            if (item is null)
            {
                return NotFound();
            }
         
            return Ok(item);

        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var item = _service.UpdateBlog(id, blog);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }
        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            var item = _service.DeleteBlog(id);

            if (item is null)
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}
