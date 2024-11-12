using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.PayRestApi.Model;
using SEPDotNetCore.Shared;
using System.Data;
using System.Data.SqlClient;

namespace SEPDotNetCore.PayRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string _connectionString = "Data Source =.;Initial Catalog=KPayDotNetCore ;User ID =sa;Password=sasa@123;TrustServerCertificate= True";
        private readonly DapperService _dapperService;

        public UserController()
        {
            _dapperService =new DapperService(_connectionString);
        }

        //[HttpGet]
        //public IActionResult GetUsers()
        //{

        //    using (IDbConnection db = new SqlConnection(_connectionString))
        //    {
        //        string query = "select * from Tbl_User where DeleteFlag = 0;";
        //        List<UserDataModel> lst = db.Query<UserDataModel>(query).ToList();

        //        return Ok(lst);
        //    }

        //}

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
           
                string query = "select * from Tbl_User  WHERE DeleteFlag = 0 AND UserId=@Id";
                var item = _dapperService.Query<UserDataModel>(query , new UserDataModel
                {
                    UserId = id
                }).FirstOrDefault();

                if(item is null)
                {
                    return NotFound();
                };

                return Ok(item);       

        }

        [HttpPost]
        public IActionResult CreateUser(UserDataModel user)
        {
            using(IDbConnection db = new SqlConnection())
            {
                string query = $@"INSERT INTO [dbo].[Tbl_User]
                   ([FullName]
                   ,[MobileNumber]
                   ,[Balance]
                   ,[pin]
                   ,[DeleteFlag])
             VALUES
                   (@FullName
                   ,@MobileNumber
                   ,@Balance
                   ,@pin
                   ,0)";

                int result = _dapperService.Execute(query, new UserDataModel
                {
                    FullName = user.FullName,
                    MobileNumber = user.MobileNumber,
                    Pin = user.Pin,
                });
                return Ok(result == 1? "Creating User SuccessFully" : " Creating User Failed");
            }
          
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id , UserDataModel user)
        {
                string query = $@"
            UPDATE [dbo].[Tbl_User]
               SET [FullName] = @FullName
                  ,[MobileNumber] = @MobileNumber
                  ,[Balance] = @Balance
                  ,[Pin] = @Pin
                  ,[DeleteFlag] = 0
             WHERE UserId = @Id";

                int result = _dapperService.Execute(query , new UserDataModel
                {
                    FullName = user.FullName,
                    MobileNumber = user.MobileNumber,
                    Pin = user.Pin,
                });

                return Ok(result == 1 ? "Updating Successful." : "Updating Fail");
            
        }

        [HttpPatch("{id}")]
        public IActionResult PatchUser(int id , UserDataModel user)
        {
            string conditions = "";
            if (!string.IsNullOrEmpty(user.FullName))
            {
                conditions += " [FullName] = @FullName, ";
            }
            if (!string.IsNullOrEmpty(user.MobileNumber))
            {
                conditions += " [MobileNumber] = @MobileNumber, ";
            }
            if (!string.IsNullOrEmpty(user.Pin))
            {
                conditions += " [Pin] = @Pin, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameters!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);


            string query = $@"UPDATE [dbo].[Tbl_User] SET {conditions} WHERE UserId = @UserId";

            int result = _dapperService.Execute(query,

                new SqlParameterModel("@UserId", id));


            if (!string.IsNullOrEmpty(user.FullName))
            {
                new SqlParameterModel("@FullName", user.FullName);
            }
            if (!string.IsNullOrEmpty(user.MobileNumber))
            {
                new SqlParameterModel("@MobileNumber",user.MobileNumber);
            }
            if (!string.IsNullOrEmpty(user.Pin))
            {
                new SqlParameterModel("@Pin", user.Pin);
            }

            return Ok(result > 0 ? "Updating Successful." : "Updating Failed...");
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            string query = "UPDATE [dbo].[Tbl_Blog] SET DeleteFlag = 1 WHERE UserId = @UserId";
            int result = _dapperService.Execute(query, new UserDataModel { UserId = id });

            return Ok(result == 0 ? "Failed Deleting User Account!" : "Successfully Deleted User");
        }

    }
}


