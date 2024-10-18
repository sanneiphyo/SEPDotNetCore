using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SEPDotNetCore.RestApi.DataModels;
using SEPDotNetCore.RestApi.ViewModels;
using System.Data;
using System.Reflection.Metadata;

namespace SEPDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source =.;Initial Catalog=SEPDotNetCore;User ID =sa;Password=sasa@123;TrustServerCertificate= True";
        
        [HttpGet]
        public IActionResult GetBlogs()
        {
          
            using (IDbConnection db = new SqlConnection(_connectionString))
            {

                string query = @"SELECT BlogId AS Id, 
                                        BlogTitle AS Title, 
                                        BlogAuthor AS Author, 
                                        BlogContent AS Content, 
                                        DeleteFlag 
                                FROM tbl_blog 
                                WHERE DeleteFlag = 0;";
                List<BlogViewModel> lst = db.Query<BlogViewModel>(query).ToList();
                return Ok(lst);
            }
          

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT BlogId AS Id, 
                                        BlogTitle AS Title, 
                                        BlogAuthor AS Author, 
                                        BlogContent AS Content, 
                                        DeleteFlag 
                                FROM tbl_blog 
                                WHERE DeleteFlag = 0  
                                AND BlogId=@Id";
                var item = db.Query<BlogViewModel>(query, new BlogViewModel
                {
                    Id = id
                }).FirstOrDefault();

                if (item is null)
                {
                    return NotFound();
                }
                return Ok(item);

            }

        }

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

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @Title
                    ,[BlogAuthor] = @Author
                    ,[BlogContent] = @Content
                    ,[DeleteFlag] = 0
                    WHERE BlogId= @Id";

                int result = db.Execute(query, new BlogViewModel
                {
                    Id = id,
                    Title = blog.Title,
                    Author = blog.Author,
                    Content = blog.Content,
                });
                return Ok(result == 1 ? "Updating Successful." : "Updating Fail");
            }

        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                if (!string.IsNullOrEmpty(blog.Title))
                {
                    conditions += " [BlogTitle] = @Title, ";
                }
                if (!string.IsNullOrEmpty(blog.Author))
                {
                    conditions += " [BlogAuthor] = @Author, ";

                }
                if (!string.IsNullOrEmpty(blog.Content))
                {
                    conditions += " [BlogContent] = @Content, ";
                }

                if (conditions.Length == 0)
                {
                    BadRequest("Invalid Parameter");
                }

                conditions = conditions.Substring(0, conditions.Length - 2);

                string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET {conditions}
                    ,[DeleteFlag] = 0
                    WHERE BlogId= @Id";

                int result = db.Execute(query, new BlogViewModel
                {
                    Id = id,
                    Title = blog.Title,
                    Author = blog.Author,
                    Content = blog.Content,
                });
                return Ok(result == 1 ? " blog Updating Successful." : "Blog Updating Failed");
            }

        }

     
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id, BlogViewModel blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "UPDATE [dbo].[Tbl_Blog] SET DeleteFlag = 1 WHERE BlogId = @BlogId";
                int result = db.Execute(query, new BlogViewModel { Id = id });

                return Ok(result == 0 ? "Deleting Blog Failed !" : "Successfully Deleted Blog");
            }
        }

        }

    }

