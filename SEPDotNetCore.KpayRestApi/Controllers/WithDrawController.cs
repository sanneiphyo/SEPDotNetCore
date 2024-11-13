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
    public class WithDrawController : ControllerBase
    {
        private readonly string _connectionString = "Data Source =.;Initial Catalog=KPayDotNetCore ;User ID =sa;Password=sasa@123;TrustServerCertificate= True";
        private readonly DapperService _dapperService;

        public WithDrawController()
        {
            _dapperService = new DapperService(_connectionString);
        }

        [HttpGet]
        public IActionResult GetWithdraws()
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from Tbl_WithDraw   where DeleteFlag = 0;";
                List<WithDrawDataModel> lst = _dapperService.Query<WithDrawDataModel>(query).ToList();

                return Ok(lst);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetWithdraw(int id)
        {

            string query = "select * from Tbl_WithDraw  WHERE DeleteFlag = 0 AND WithDrawId=@Id";
            var item = _dapperService.Query<WithdrawDataModel>(query, new WithdrawDataModel
            {
                WithdrawId = id

            }).FirstOrDefault();

            if (item is null)
            {
                return NotFound();
            };

            return Ok(item);

        }

        [HttpPost]
        public IActionResult CreateWithdraw(WithdrawDataModel withdraw)
        {
            string getBalanceQuery = "SELECT Balance FROM Tbl_User WHERE MobileNumber = @MobileNumber AND DeleteFlag = 0";
            var currentBalance = _dapperService.Query<WithdrawDataModel>(getBalanceQuery, new WithdrawDataModel
            {
                MobileNumber = withdraw.MobileNumber

            }).FirstOrDefault();

            if (currentBalance is null)
            {
                return BadRequest("No Data Found");
            }

           
            if (currentBalance.Balance < withdraw.Balance)
            {
                return BadRequest("Insufficient balance.");
            }
            var newBalance = currentBalance.Balance - withdraw.Balance;


            string updateBalanceQuery = "UPDATE Tbl_User SET Balance = @NewBalance WHERE MobileNumber = @MobileNumber AND DeleteFlag = 0";
            int updateResult = _dapperService.Execute(updateBalanceQuery, new WithdrawDataModel
            {
                Balance = newBalance,
                MobileNumber = withdraw.MobileNumber
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

            int insertResult = _dapperService.Execute(query, new WithdrawDataModel
            {
                MobileNumber = withdraw.MobileNumber,
                Balance = withdraw.Balance
            });

            return Ok(insertResult == 0 ? "Deposit is not completed" : "Deposit completed successfully.");

        }



        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            string query = "UPDATE [dbo].[Tbl_Deposit] SET DeleteFlag = 1 WHERE WithDrawId = @WithDrawId";
            int result = _dapperService.Execute(query, new WithdrawDataModel { WithdrawId = id });

            return Ok(result == 0 ? "Failed Deleting Deposit!" : "Successfully Deleted Deposit");
        }
    }
}
