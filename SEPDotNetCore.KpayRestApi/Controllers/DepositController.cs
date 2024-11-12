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
    public class DepositController : ControllerBase
    {
        private readonly string _connectionString = "Data Source =.;Initial Catalog=KPayDotNetCore ;User ID =sa;Password=sasa@123;TrustServerCertificate= True";
        private readonly DapperService _dapperService;

        public DepositController()
        {
            _dapperService = new DapperService(_connectionString);
        }


        [HttpGet]
        public IActionResult GetUsers()
        {

            using (IDbConnection db = new SqlConnection(_connectionString)) 
            {
                string query = "select * from Tbl_Deposit   where DeleteFlag = 0;";
                List<DepositDataModel> lst = _dapperService.Query<DepositDataModel>(query).ToList();

                return Ok(lst);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {

            string query = "select * from Tbl_Deposit  WHERE DeleteFlag = 0 AND DepositId=@Id";
            var item = _dapperService.Query<DepositDataModel>(query, new DepositDataModel
            {
                DepositId = id

            }).FirstOrDefault();

            if (item is null)
            {
                return NotFound();
            };

            return Ok(item);

        }

        [HttpPost]
        public IActionResult CreateUser(DepositDataModel deposit)
        {
            using (IDbConnection db = new SqlConnection())
            {
                string query = $@"INSERT INTO [dbo].[Tbl_Deposit]
                   ([MobileNumber]
                   ,[Balance]
                   ,[DeleteFlag])
             VALUES
                   (@MobileNumber
                   ,@Balance
                   ,0)";

                int result = _dapperService.Execute(query, new DepositDataModel
                {
                    MobileNumber = deposit.MobileNumber,
                    Balance = deposit.Balance,
                  
                });
                return Ok(result == 1 ? "Creating User SuccessFully" : " Creating User Failed");
            }
               
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, DepositDataModel deposit)
        {
            string query = $@"
            UPDATE [dbo].[Tbl_Deposit]
               SET [MobileNumber] = @MobileNumber
                  ,[Balance] = @Balance
                  ,[DeleteFlag] = 0
             WHERE DepositId = @Id";

            int result = _dapperService.Execute(query, new DepositDataModel
            {
             
                MobileNumber = deposit.MobileNumber,
                Balance = deposit.Balance,
            });

            return Ok(result == 1 ? "Updating Successful." : "Updating Fail");

        }


        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            string query = "UPDATE [dbo].[Tbl_Deposit] SET DeleteFlag = 1 WHERE DepositId = @DepositId";
            int result = _dapperService.Execute(query, new DepositDataModel { DepositId = id });

            return Ok(result == 0 ? "Failed Deleting User Account!" : "Successfully Deleted User");
        }

    }
}
