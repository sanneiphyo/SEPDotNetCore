using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SEODotNetCore.TodoList.DataModels;
using SEODotNetCore.TodoList.ViewModels;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SEODotNetCore.TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskCategoryController : ControllerBase

    {
        private readonly string _connectionString = "Data Source =.;Initial Catalog=TDLDotNetCore;User ID =sa;Password=sasa@123;TrustServerCertificate= True";
        [HttpGet]
        public IActionResult GetTaskCategory()
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT [CategoryId]
                          ,[CategoryName]
                          ,[DeleteFlag]
                      FROM [dbo].[TaskCategory] WHERE DeleteFlag = 0";

                List<TaskCategoryViewModel> lst = db.Query<TaskCategoryViewModel>(query).ToList();
                return Ok(lst);
            }         

        }

        [HttpGet("{id}")]
        public IActionResult GetTaskCategoryById(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT [CategoryId]
                          ,[CategoryName]
                          ,[DeleteFlag]
                      FROM [dbo].[TaskCategory] WHERE DeleteFlag = 0";

                var item = db.Query(query, new TaskCategoryViewModel
                {
                    CategoryId = id
                }).FirstOrDefault();

                if(item is null)
                {
                    return NotFound();
                };

                return Ok(item);
            }
             

        }

        [HttpPost]
        public IActionResult CreateTaskCategory(TaskCategoryDataModel Task)
        {
            string query = $@"
                INSERT INTO [dbo].[TaskCategory]
                           ([CategoryName]
                           ,[DeleteFlag])
                     VALUES
                           (@CategoryName
                           , 0)";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new TaskCategoryDataModel
                {
                    CategoryId = Task.CategoryId,
                    CategoryName = Task.CategoryName
                });

                return Ok(result == 1? "TaskCategory Created" : "TaskCategory Creating Failed");
            }

              
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTaskCategory(int id, TaskCategoryViewModel Task)
        {
            string query = $@"
                UPDATE [dbo].[TaskCategory]
                   SET [CategoryName] = @CategoryName
                      ,[DeleteFlag] = 0
                 WHERE CategoryId = @Id ";
            using(IDbConnection db = new SqlConnection(_connectionString))

            {
                int result = db.Execute(query, new TaskCategoryViewModel
                {
                    CategoryId = Task.CategoryId,
                    CategoryName = Task.CategoryName
                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Failed");
            }
          

        }

        [HttpPatch("{id}")]
        public IActionResult PatchTaskCategory(int id , TaskCategoryViewModel Task)
        {

            string conditions = "";
            if (!string.IsNullOrEmpty(Task.CategoryName))
            {
                conditions += " [CategoryName] = @CategoryName ";
            }
          
            if (conditions.Length == 0)
            {
                BadRequest("Invalid Parameter");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            using (IDbConnection db = new SqlConnection(_connectionString))

            {
                string query = $@"
                UPDATE [dbo].[TaskCategory]
                   SET{conditions}
                      ,[DeleteFlag] = 0
                 WHERE CategoryId = @Id ";
                int result = db.Execute(query, new TaskCategoryViewModel
                {
                    CategoryId = Task.CategoryId,
                    CategoryName = Task.CategoryName
                });
                return Ok(result == 1 ? "Updating Successful" : "Updating Failed");
            }

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteTaskCategory(int id , TaskCategoryViewModel Task)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"DELETE FROM [dbo].[TaskCategory] WHERE DeleteFlag = 1";
                int result = db.Execute(query, new TaskCategoryViewModel { CategoryId = Task.CategoryId });
                return Ok(result == 0 ? "Deleting TaskCategory Failed" : "TaskCategory Successfully Deleted");
            }
         
        }
    }
}
