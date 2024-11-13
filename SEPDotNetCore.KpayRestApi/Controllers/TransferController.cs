using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.PayRestApi.Model;
using SEPDotNetCore.Shared;

namespace SEPDotNetCore.PayRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {

        private readonly string _connectionString = "Data Source =.;Initial Catalog=KPayDotNetCore ;User ID =sa;Password=sasa@123;TrustServerCertificate= True";
        private readonly DapperService _dapperService;

        public TransferController()
        {
            _dapperService = new DapperService(_connectionString);
        }


        [HttpGet]
        public IActionResult GetUsers()
        {

            string query = "select * from Tbl_Transfer   where DeleteFlag = 0;";
            List<TransferDataModel> lst = _dapperService.Query<TransferDataModel>(query).ToList();

            return Ok(lst);


        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {

            string query = "select * from Tbl_Transfer  WHERE DeleteFlag = 0 AND TransferId=@Id";
            var transfer = _dapperService.Query<TransferDataModel>(query, new TransferDataModel
            {
                TransferId = id

            }).FirstOrDefault();

            if (transfer is null)
            {
                return NotFound();
            };

            return Ok(transfer);

        }

        [HttpPost]
        public IActionResult CreateTransfer(TransferDataModel transfer)
        {
            string senderQuery = "SELECT * FROM Tbl_User WHERE MobileNumber = @MobileNumber AND DeleteFlag = 0";
            string receiverQuery = "SELECT * FROM Tbl_User WHERE MobileNumber = @MobileNumber AND DeleteFlag = 0";

            var sender = _dapperService.Query<UserDataModel>(senderQuery, new
            {
                MobileNumber = transfer.FromMobileNumber
            }).FirstOrDefault();

            var receiver = _dapperService.Query<UserDataModel>(receiverQuery, new
            {
                MobileNumber = transfer.ToMobileNumber
            }).FirstOrDefault();

            if (sender is null)
                return BadRequest("Invalid Mobile Number.");

            if (receiver is null)
                return BadRequest("Invalid Mobile Number.");

            if (transfer.FromMobileNumber == transfer.ToMobileNumber)
                return BadRequest("Sender and receiver mobile numbers must be different.");

            if (sender.Pin != transfer.Pin.ToString())
                return Unauthorized("Invalid PIN.");

            if (sender.Balance < transfer.Amount)
                return BadRequest("Insufficient balance.");

           
            sender.Balance -= transfer.Amount;
            receiver.Balance += transfer.Amount;


            string query = $@"INSERT INTO [dbo].[Tbl_Transfer]
                            ([FromMobileNumber]
                            ,[ToMobileNumber]
                            ,[Amount]
                            ,[Pin]
                            ,[DeleteFlag])
                      VALUES
                            (@FromMobileNumber)
                            ,@ToMobileNumber
                            ,@Amount
                            ,@Pin
                            ,0)";


            var result = _dapperService.Execute(query, new TransferDataModel
            {
                FromMobileNumber = transfer.FromMobileNumber,
                ToMobileNumber = transfer.ToMobileNumber,
                Amount = transfer.Amount,
                Pin = transfer.Pin
            });

            return Ok(result > 0 ? "Transfer completed successfully." : "An error occurred while processing the transfer");
         
           
        }

    }

}
   