using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SEPDotNetCore.RestApi.DataModels;
using SEPDotNetCore.RestApi.ViewModels;
using SEPDotNetCore.Shared;
using System.Data;

namespace SEPDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController2 : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        private readonly DapperService _dapperService;

        public BlogDapperController2()
        {
            _dapperService = new DapperService(_connectionString);
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {        
                string query = @"SELECT BlogId AS Id, 
                                        BlogTitle AS Title, 
                                        BlogAuthor AS Author, 
                                        BlogContent AS Content, 
                                        DeleteFlag 
                                FROM tbl_blog 
                                WHERE DeleteFlag = 0;";
                List<BlogViewModel> lst = _dapperService.Query<BlogViewModel>(query).ToList();
                return Ok(lst);
            }


       
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
          
                string query = @"SELECT BlogId AS Id, 
                                        BlogTitle AS Title, 
                                        BlogAuthor AS Author, 
                                        BlogContent AS Content, 
                                        DeleteFlag 
                                FROM tbl_blog 
                                WHERE DeleteFlag = 0  
                                AND BlogId=@Id";
                var item = _dapperService.Query<BlogViewModel>(query, new BlogViewModel
                {
                    Id = id
                }).FirstOrDefault();

                if (item is null)
                {
                    return NotFound();
                }
                return Ok(item);

            

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
                int result = _dapperService.Execute(query, new BlogDataModel
                {
                    BlogTitle = blog.BlogTitle,
                    BlogAuthor = blog.BlogAuthor,
                    BlogContent = blog.BlogContent
                });
                return Ok(result == 1 ? "Saving Successful " : "Saving failed");
            }


        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                    SET [BlogTitle] = @Title
                    ,[BlogAuthor] = @Author
                    ,[BlogContent] = @Content
                    ,[DeleteFlag] = 0
                    WHERE BlogId= @Id";

                int result = _dapperService.Execute(query, new BlogViewModel
                {
                    Id = id,
                    Title = blog.Title,
                    Author = blog.Author,
                    Content = blog.Content,
                });
                return Ok(result == 1 ? "Updating Successful." : "Updating Fail");
            }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id, BlogViewModel blog)
        {
         
                string query = "UPDATE [dbo].[Tbl_Blog] SET DeleteFlag = 1 WHERE BlogId = @BlogId";
                int result = _dapperService.Execute(query, new BlogViewModel { Id = id });

                return Ok(result == 0 ? "Deleting Blog Failed !" : "Successfully Deleted Blog");
            
        }

    }
    }
    

