using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;


namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetPayments()
        {
            var lst = _db.PaymentDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetPayment(int id)
        {
            var item = _db.PaymentDataModel.AsNoTracking().FirstOrDefault(x => x.PaymentId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreatePayment(PaymentDataModel payment)
        {
            _db.PaymentDataModel.Add(payment);
            _db.SaveChanges();
            return Ok(new { Message = "Create Payment" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePayment(int id, PaymentDataModel payment)
        {
            var item = _db.PaymentDataModel.AsNoTracking().FirstOrDefault(x => x.PaymentId == id);

            if (item is null)
            {
                return NotFound();
            }
            item.Amount = payment.Amount;
            item.PaymentMethod = payment.PaymentMethod;


            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

        [HttpDelete]
        public IActionResult DeletePayment(int id)
        {
            var item = _db.PaymentDataModel.AsNoTracking().FirstOrDefault(x => x.PaymentId == id);


            if (item is null)
            {
                return NotFound();
            }
            item.DeleteFlag = true;

            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return Ok(item);
        }
    }
}
