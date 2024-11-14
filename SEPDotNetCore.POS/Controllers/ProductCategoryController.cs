using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;

namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetProductCategories()
        {
            var lst = _db.ProductCategoryDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductCategory(int id)
        {
            var item = _db.ProductCategoryDataModel.AsNoTracking().FirstOrDefault(x => x.ProductCategoryId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateProductCategory(ProductCategoryDataModel category)
        {
            _db.ProductCategoryDataModel.Add(category);
            _db.SaveChanges();
            return Ok(new { Message = "GetBlogs" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProductCategory(int id, ProductCategoryDataModel category)
        {
            var item = _db.ProductCategoryDataModel.AsNoTracking().FirstOrDefault(x => x.ProductCategoryId == id);

            if (item is null)
            {
                return NotFound();
            }
           
            item.ProductCategoryCode = category.ProductCategoryCode;
            item.ProductCategoryName = category.ProductCategoryName;
          

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

        [HttpPatch("{id}")]
        public IActionResult PatchProductCategory(int id, ProductCategoryDataModel category)
        {
            var item = _db.ProductCategoryDataModel.AsNoTracking().FirstOrDefault(x => x.ProductCategoryId == id);

            if (item is null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(category.ProductCategoryCode))
            {
                item.ProductCategoryCode = category.ProductCategoryCode;
            }
            if (!string.IsNullOrEmpty(category.ProductCategoryCode))
            {
                item.ProductCategoryName = category.ProductCategoryName;
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }
        [HttpDelete]
        public IActionResult DeleteProductCategory(int id)
        {
            var item = _db.ProductCategoryDataModel.AsNoTracking().FirstOrDefault(x => x.ProductCategoryId == id);

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
