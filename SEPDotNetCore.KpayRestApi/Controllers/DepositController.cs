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
        public IActionResult GetDeposits()
        {

             string query = "select * from Tbl_Deposit   where DeleteFlag = 0;";
                List<DepositDataModel> lst = _dapperService.Query<DepositDataModel>(query).ToList();

                return Ok(lst);
            

        }

        [HttpGet("{id}")]
        public IActionResult GetDeposit(int id)
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
        public IActionResult CreateDeposit(DepositDataModel deposit)
        {

            string getBalanceQuery = "SELECT Balance FROM Tbl_User WHERE MobileNumber = @MobileNumber AND DeleteFlag = 0";
            var currentBalance = _dapperService.Query<DepositDataModel>(getBalanceQuery, new DepositDataModel
            {
                MobileNumber = deposit.MobileNumber

            }).FirstOrDefault();

            if (currentBalance is null)
            {
                return BadRequest("Invalid mobile number. User not found!");
            }


            var newBalance = currentBalance.Balance + deposit.Balance;


            string updateBalanceQuery = "UPDATE Tbl_User SET Balance = @NewBalance WHERE MobileNumber = @MobileNumber AND DeleteFlag = 0";
            int updateResult = _dapperService.Execute(updateBalanceQuery, new DepositDataModel
            {
                Balance = newBalance,
                MobileNumber = deposit.MobileNumber
            });

            if (updateResult == 0)
            {
                return NotFound("No Data Found");
                  
            }

            string query = $@"
            UPDATE [dbo].[Tbl_Deposit]
               SET [MobileNumber] = @MobileNumber
                  ,[Balance] = @Balance
                  ,[DeleteFlag] = 0
             WHERE DepositId = @Id";

            int insertResult = _dapperService.Execute(query, new DepositDataModel
            {
                MobileNumber = deposit.MobileNumber,
                Balance = deposit.Balance
            });

            return Ok(insertResult == 0 ? "Deposit is not completed" : "Deposit completed successfully.");
           
        }

    }
}
