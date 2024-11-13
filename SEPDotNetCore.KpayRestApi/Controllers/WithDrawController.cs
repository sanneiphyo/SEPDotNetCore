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
        public IActionResult GetWithDraws()
        {

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * from Tbl_WithDraw   where DeleteFlag = 0;";
                List<WithDrawDataModel> lst = _dapperService.Query<WithDrawDataModel>(query).ToList();

                return Ok(lst);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetWithDraw(int id)
        {

            string query = "select * from Tbl_WithDraw  WHERE DeleteFlag = 0 AND WithDrawId=@Id";
            var item = _dapperService.Query<WithDrawDataModel>(query, new WithDrawDataModel
            {
                WithDrawId = id

            }).FirstOrDefault();

            if (item is null)
            {
                return NotFound();
            };

            return Ok(item);

        }

        [HttpPost]
        public IActionResult CreateWithDraw(WithDrawDataModel withdraw)
        {
            using (IDbConnection db = new SqlConnection())
            {
                string query = $@"INSERT INTO [dbo].[Tbl_WithDraw]
                   ([MobileNumber]
                   ,[Balance]
                   ,[DeleteFlag])
             VALUES
                   (@MobileNumber
                   ,@Balance
                   ,0)";

                int result = _dapperService.Execute(query, new DepositDataModel
                {
                    MobileNumber = withdraw.MobileNumber,
                    Balance = withdraw.Balance,

                });
                return Ok(result == 1 ? "WithDraw SuccessFully Created" : "WithDraw  Failed");
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, WithDrawDataModel withdraw)
        {
            string query = $@"
            UPDATE [dbo].[Tbl_WithDraw]
               SET [MobileNumber] = @MobileNumber
                  ,[Balance] = @Balance
                  ,[DeleteFlag] = 0
             WHERE DepositId = @Id";

            int result = _dapperService.Execute(query, new WithDrawDataModel
            {

                MobileNumber = withdraw.MobileNumber,
                Balance = withdraw.Balance,
            });

            return Ok(result == 1 ? "Updating Successful." : "Updating Fail");

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            string query = "UPDATE [dbo].[Tbl_Deposit] SET DeleteFlag = 1 WHERE WithDrawId = @WithDrawId";
            int result = _dapperService.Execute(query, new WithDrawDataModel { WithDrawId = id });

            return Ok(result == 0 ? "Failed Deleting Deposit!" : "Successfully Deleted Deposit");
        }
    }
}
