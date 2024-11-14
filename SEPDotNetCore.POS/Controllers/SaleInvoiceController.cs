using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;

namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleInvoiceController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();



        [HttpGet]
        public IActionResult GetSaleInvoices()
        {
            var lst = _db.SaleInvoiceDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetSaleInvoice(int id)
        {
            var item = _db.SaleInvoiceDataModel.AsNoTracking().FirstOrDefault(x => x.SaleInvoiceId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateSaleInvoice(SaleInvoiceDataModel invoice)
        {
            _db.SaleInvoiceDataModel.Add(invoice);
            _db.SaveChanges();
            return Ok(new { Message = "Create Invoice Data" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSaleInvoice(int id, SaleInvoiceDataModel invoice)
        {
            var item = _db.SaleInvoiceDataModel.AsNoTracking().FirstOrDefault(x => x.SaleInvoiceId == id);

            if (item is null)
            {
                return NotFound();
            }

            item.SaleInvoiceDateTime = invoice.SaleInvoiceDateTime;
            item.VoucherNo = invoice.VoucherNo;
            item.TotalAmount = invoice.TotalAmount;
            item.Discount =  invoice.Discount;
            item.StaffCode = invoice.StaffCode;
            item.Tax = invoice.Tax;
            item.PaymentType = invoice.PaymentType;
            item.ReceiveAmount = invoice.ReceiveAmount;
            item.Change = invoice.Change;
            item.CustomerCode = invoice.CustomerCode;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

   
        [HttpDelete]
        public IActionResult DeleteSaleInvoice(int id)
        {
            var item = _db.SaleInvoiceDataModel.AsNoTracking().FirstOrDefault(x => x.SaleInvoiceId == id);

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
