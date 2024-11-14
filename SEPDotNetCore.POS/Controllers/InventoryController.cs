using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;

namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var lst = _db.InventoryDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var item = _db.InventoryDataModel.AsNoTracking().FirstOrDefault(x => x.InventoryId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateCustomer(InventoryDataModel inventory)
        {
            _db.InventoryDataModel.Add(inventory);
            _db.SaveChanges();
            return Ok(new { Message = "Create customer" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, InventoryDataModel inventory)
        {
            var item = _db.InventoryDataModel.AsNoTracking().FirstOrDefault(x => x.InventoryId == id);

            if (item is null)
            {
                return NotFound();
            }
            item.Quantity = inventory.Quantity;
          

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            var item = _db.InventoryDataModel.AsNoTracking().FirstOrDefault(x => x.InventoryId == id);

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
