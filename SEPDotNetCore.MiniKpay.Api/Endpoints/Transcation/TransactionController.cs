using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.MiniKpay.Domain.features.Transactions;
using SEPDotNetCore.MiniKpay.Domain.Models;

namespace SEPDotNetCore.MiniKpay.Api.Endpoints.Transaction
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : baseController //baseController က parent ဖြစ် ပြီးTransactionController  က child ဖြစ်သွားတယ်
    {

        private readonly TransactionService _service;

        public TransactionController(TransactionService service)
        {
            _service = service;
        }

        [HttpPost("Transfer")]
        public IActionResult Transfer(TransferRequestModel transferRequestModel)
        {
            var model = _service.Transfer(transferRequestModel.SenderId, transferRequestModel.ReceiverId, transferRequestModel.Amount);

            return Execute(model);
        }

       
    }
}
