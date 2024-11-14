using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;

namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetSuppliers()
        {
            var lst = _db.SupplierDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetSupplier(int id)
        {
            var item = _db.SupplierDataModel.AsNoTracking().FirstOrDefault(x => x.SupplierId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateSupplier(SupplierDataModel supplier)
        {
            _db.SupplierDataModel.Add(supplier);
            _db.SaveChanges();
            return Ok(new { Message = "Create Product" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSupplier(int id, SupplierDataModel supplier)
        {
            var item = _db.SupplierDataModel.AsNoTracking().FirstOrDefault(x => x.SupplierId == id);

            if (item is null)
            {
                return NotFound();
            }
            item.Name = supplier.Name;
            item.Contact = supplier.Contact;
            item.Email = supplier.Email;
            item.Address =supplier.Address;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

        [HttpPatch("{id}")]
        public IActionResult PatchSupplier(int id, SupplierDataModel supplier)
        {
            var item = _db.SupplierDataModel.AsNoTracking().FirstOrDefault(x => x.SupplierId == id);

            if (item is null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(supplier.Name))
            {
                item.Name = supplier.Name;
            }
            if (!string.IsNullOrEmpty(supplier.Contact))
            {
                item.Contact = supplier.Contact;
            }
            if (!string.IsNullOrEmpty(supplier.Email))
            {
                item.Email = supplier.Email;
            }
            if (!string.IsNullOrEmpty(supplier.Address))
            {
                item.Address = supplier.Address;
            }



            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }
        [HttpDelete]
        public IActionResult DeleteSupplier(int id)
        {
            var item = _db.SupplierDataModel.AsNoTracking().FirstOrDefault(x => x.SupplierId == id);

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
