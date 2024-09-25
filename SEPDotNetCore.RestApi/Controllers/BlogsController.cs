using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SEPDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            return Ok();  
        }


        [HttpPost]
        public IActionResult CreateBlog()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateBlog()
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult PatchBlog()
        {
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteBlogs()
        {
            return Ok();
        }
    }
}
