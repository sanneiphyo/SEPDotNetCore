using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SEPDotNetCore.RestApi.DataModels;
using SEPDotNetCore.RestApi.ViewModels;
using System.Data;

namespace SEPDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDpperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source =.;Initial Catalog=SEPDotNetCore;User ID =sa;Password=sasa@123";
     
        
        //need to test
        [HttpGet]
        public IActionResult GetBlogs()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from tbl_blog where DeleteFlag = 0;";
                var lst = db.Query<BlogViewModel>(query)
                            .ToList(); 

                foreach (var item in lst)
                {
                    Console.WriteLine(item.Id);
                    Console.WriteLine(item.Title);
                    Console.WriteLine(item.Author);
                    Console.WriteLine(item.Content);
                }
                return Ok(lst);

            }

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog()
        {
            return Ok();

        }
        //need to test
        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = blog.BlogTitle,
                    BlogAuthor = blog.BlogAuthor,
                    BlogContent = blog.BlogContent
                });
                return Ok(result == 1 ? "Saving Successful " : "Saving faileds");
            }


        }
        //need to test
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(BlogViewModel blog)
        {

            string query = $@"UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
             ,[BlogContent] = @BlogContent
             ,[DeleteFlag] = 0
            WHERE BlogId = @BlogId";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogViewModel
                {
                  
                    Title = blog.Title,
                    Author = blog.Author, 
                    Content = blog.Content
                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Failed");
            }


        }

        //need to test
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id , BlogViewModel blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))

            {
                string conditions = "";
                if (!string.IsNullOrEmpty(blog.Title))
                {
                    conditions += " [BlogTitle] = @BlogTitle, ";
                }
                if (!string.IsNullOrEmpty(blog.Author))
                {
                    conditions += " [BlogAuthor] = @BlogAuthor, ";
                }
                if (!string.IsNullOrEmpty(blog.Content))
                {
                    conditions += " [BlogContent] = @BlogContent, ";
                }

                if (conditions.Length == 0)
                {
                    return BadRequest("Invalid Parameters!");
                }

                conditions = conditions.Substring(0, conditions.Length - 2);

                string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";


                int result = db.Execute(query, new BlogViewModel
                {
                    Title = blog.Title,
                    Author = blog.Author,
                    Content = blog.Content
                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Failed");
            }

        }
        
        //need to test
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
          
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE [dbo].[Tbl_Blog] SET DeleteFlag = 1 WHERE BlogId = @BlogId";
                int result = db.Execute(query, new BlogViewModel
                {
                    Id = id,
                });

                return Ok(result == 0 ? "Deleting Blog Failed !" : "Successfully Blog Failed");
            }
        }

    }
}
