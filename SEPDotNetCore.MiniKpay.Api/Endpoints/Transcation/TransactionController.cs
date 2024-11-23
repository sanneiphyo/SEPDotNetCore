using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.MiniKpay.Domain.features.Transactions;
using SEPDotNetCore.MiniKpay.Domain.Models;

namespace SEPDotNetCore.MiniKpay.Api.Endpoints.Transaction
{
    [Route("api/Transaction/[controller]")]
    [ApiController]
    public class TransactionController : baseController //baseController က parent ဖြစ် ပြီးTransactionController  က child ဖြစ်သွားတယ်
    {

        private readonly TransactionService _service;

        public TransactionController(TransactionService service)
        {
            _service = service;
        }

        [HttpPost("Transfer")]
        public async Task<IActionResult> Transfer(TransferRequestModel transferRequestModel)
        {
            var model = await _service.Transfer(transferRequestModel.SenderId, transferRequestModel.ReceiverId, transferRequestModel.Amount);

            return Execute(model);
        }

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw( TransactionRequestModel requestModel)
        {
           
             await  _service.Withdraw(requestModel.UserId, requestModel.Amount);
            return NoContent();

        }

        [HttpPost("deposit")]
        public  async Task<IActionResult> Deposit(TransactionRequestModel requestModel)
        {

            await _service.Deposit(requestModel.UserId, requestModel.Amount);
            return NoContent();

        }

    }
}
