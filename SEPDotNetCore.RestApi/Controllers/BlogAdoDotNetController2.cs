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
    public class BlogAdoDotNetController2 : ControllerBase
    {
        private readonly string _connectionString;
        private readonly AdoDotNetService _adoDotNetService;

        public BlogAdoDotNetController2(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection")!;
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }


     
        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();
            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {

                lst.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(dr["BlogId"]),
                    Title = Convert.ToString(dr["BlogTitle"]),
                    Author = Convert.ToString(dr["BlogAuthor"]),
                    Content = Convert.ToString(dr["BlogContent"]),
                    DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"]),
                });
            }
            return Ok(lst);
        }


        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {

            string query = @"SELECT [BlogId]
                                ,[BlogTitle]
                                ,[BlogAuthor]
                                ,[BlogContent]
                                ,[DeleteFlag]
                            FROM [dbo].[Tbl_Blog] 
                            WHERE BlogId = @BlogId";

            var dt = _adoDotNetService.Query(query,
            new SqlParameterModel("@id", id));

            if (dt.Rows.Count == 0)
            {
                return NotFound();

            }
            DataRow dr = dt.Rows[0];
            var item = new BlogViewModel
            {
                Id = Convert.ToInt32(dr["BlogId"]),
                Title = Convert.ToString(dr["BlogTitle"]),
                Author = Convert.ToString(dr["BlogAuthor"]),
                Content = Convert.ToString(dr["BlogContent"]),
                DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"]),
            };
           
            return Ok(item);

        }


        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {

            string query = @"
                    INSERT INTO [dbo].[Tbl_blog]
                    ([BlogTitle]
                    ,[BlogAuthor]
                    ,[BlogContent]
                    ,[DeleteFlag])
             VALUES
                    (@BlogTitle
                    ,@BlogAuthor
                    ,@BlogContent
                       ,0
		               )";
            int result = _adoDotNetService.Execute(query,
            new SqlParameterModel("@BlogTitle", blog.BlogTitle),
            new SqlParameterModel("@BlogAuthor", blog.BlogAuthor),
            new SqlParameterModel("@BlogContent", blog.BlogContent));

            return Ok(result == 1 ? "Saving Successful " : "Saving faileds");

        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {

        
            string query = $@"UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
             ,[BlogContent] = @BlogContent
             ,[DeleteFlag] = 0
            WHERE BlogId = @BlogId";

           int result = _adoDotNetService.Execute(query,
          new SqlParameterModel("@BlogTitle", id),
          new SqlParameterModel("@BlogTitle", blog.Title),
          new SqlParameterModel("@BlogAuthor", blog.Author),
          new SqlParameterModel("@BlogContent", blog.Content));

            return Ok(result == 1 ? "Updating Successful" : "Updating Failed");

        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
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

            int result = _adoDotNetService.Execute(query,

                new SqlParameterModel("@BlogTitle", id));
         

            if (!string.IsNullOrEmpty(blog.Title))
            {
                new SqlParameterModel("@BlogTitle", blog.Title);
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                new SqlParameterModel("@BlogAuthor", blog.Author);
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                new SqlParameterModel("@BlogContent", blog.Content);
            }

            return Ok(result > 0 ? "Updating Successful." : "Updating Failed...");
        }
        
        


        [HttpDelete]
        public IActionResult DeleteBlog(int id, BlogViewModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [DeleteFlag] = 1
             WHERE BlogId = @id";

            int result = _adoDotNetService.Execute(query,
             new SqlParameterModel("@id", id));

            return Ok(result == 0 ? "Deleting Blog Failed! " : "Successfully Deleted Blog");
        }
    }
}
