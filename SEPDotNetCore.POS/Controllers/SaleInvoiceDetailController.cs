using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;

namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleInvoiceDetailController : ControllerBase
    {

        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetSaleInvoiceDetails()
        {
            var lst = _db.SaleInvoiceDetailDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetSaleInvoiceDetail(int id)
        {
            var item = _db.SaleInvoiceDetailDataModel.AsNoTracking().FirstOrDefault(x => x.SaleInvoiceDetailId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateSaleInvoiceDetail(SaleInvoiceDetailDataModel detail)
        {
            _db.SaleInvoiceDetailDataModel.Add(detail);
            _db.SaveChanges();
            return Ok(new { Message = "Create SaleInvoiceDetail" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, SaleInvoiceDetailDataModel detail)
        {
            var item = _db.SaleInvoiceDetailDataModel.AsNoTracking().FirstOrDefault(x => x.SaleInvoiceDetailId == id);

            if (item is null)
            {
                return NotFound();
            }
            item.SaleInvoiceDateTime = detail.SaleInvoiceDateTime;
            item.VoucherNo = detail.VoucherNo;
            item.ProductCode = detail.ProductCode;
            item.Quantity = detail.Quantity;
            item.Price = detail.Price;
            item.Amount = detail.Amount;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

    
        
        [HttpDelete]
        public IActionResult DeleteSaleInvoiceDetail(int id)
        {
        var item = _db.SaleInvoiceDetailDataModel.AsNoTracking().FirstOrDefault(x => x.SaleInvoiceDetailId == id);

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
