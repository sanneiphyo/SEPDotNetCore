using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;


namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetProducts()
        {
            var lst = _db.ProductDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var item = _db.ProductDataModel.AsNoTracking().FirstOrDefault(x => x.ProductId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductDataModel product  )
        {
            _db.ProductDataModel.Add(product);
            _db.SaveChanges();
            return Ok(new { Message = "Create Product" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductDataModel product)
        {
            var item = _db.ProductDataModel.AsNoTracking().FirstOrDefault(x => x.ProductId == id);

            if (item is null)
            {
                return NotFound();
            }
            item.ProductCode = product.ProductCode;
            item.ProductCategoryCode = product.ProductCategoryCode;
            item.ProductName = product.ProductName;
            item.Price = product.Price;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

        [HttpPatch("{id}")]
        public IActionResult PatchProduct(int id, ProductDataModel product)
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
        public IActionResult DeleteProduct(int id)
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

