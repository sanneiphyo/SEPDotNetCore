using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;


namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var lst = _db.CustomerDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(int id)
        {
            var item = _db.CustomerDataModel.AsNoTracking().FirstOrDefault(x => x.CustomerId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateCustomer(CustomerDataModel customer)
        {
            _db.CustomerDataModel.Add(customer);
            _db.SaveChanges();
            return Ok(new { Message = "Create customer" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, CustomerDataModel customer)
        {
            var item = _db.CustomerDataModel.AsNoTracking().FirstOrDefault(x => x.CustomerId == id);

            if (item is null)
            {
                return NotFound();
            }
            item.FirstName = customer.FirstName;
            item.LastName = customer.LastName;
            item.Email = customer.Email;
            item.PhoneNumber = customer.PhoneNumber;
            item.Address= customer.Address;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

        [HttpPatch("{id}")]
        public IActionResult PatchCustomer(int id, ProductDataModel product)
        {
            var item = _db.ProductDataModel.AsNoTracking().FirstOrDefault(x => x.ProductId == id);

            if (item is null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(product.ProductCode))
            {
                item.ProductCode = product.ProductCode;
            }
            if (!string.IsNullOrEmpty(product.ProductCategoryCode))
            {
                item.ProductCategoryCode = product.ProductCategoryCode;
            }
            if (!string.IsNullOrEmpty(product.ProductName))
            {
                item.ProductName = product.ProductName;
            }


            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }
        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            var item = _db.ProductDataModel.AsNoTracking().FirstOrDefault(x => x.ProductId == id);

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
