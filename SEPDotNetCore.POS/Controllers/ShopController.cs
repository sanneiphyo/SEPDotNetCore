using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;
namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();



        [HttpGet]
        public IActionResult GetShops()
        {
            var lst = _db.ShopDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetShop(int id)
        {
            var item = _db.ShopDataModel.AsNoTracking().FirstOrDefault(x => x.ShopId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateShop(ShopDataModel shop)
        {
            _db.ShopDataModel.Add(shop);
            _db.SaveChanges();
            return Ok(new { Message = "Create Shop Data" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateShop(int id, ShopDataModel shop)
        {
            var item = _db.ShopDataModel.AsNoTracking().FirstOrDefault(x => x.ShopId == id);

            if (item is null)
            {
                return NotFound();
            }

            item.ShopCode = shop.ShopCode;
            item.ShopName = shop.ShopName;
            item.MobileNo = shop.MobileNo;
            item.Address = shop.Address;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

        [HttpPatch("{id}")]
        public IActionResult PatchShop(int id, ShopDataModel shop)
        {
            var item = _db.ShopDataModel.AsNoTracking().FirstOrDefault(x => x.ShopId == id);

            if (item is null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(shop.ShopCode))
            {
                item.ShopCode = shop.ShopCode;
            }
            if (!string.IsNullOrEmpty(shop.ShopName))
            {
                item.ShopName = shop.ShopName;
            }
            if (!string.IsNullOrEmpty(shop.MobileNo))
            {
                item.MobileNo = shop.MobileNo;
            }
            if (!string.IsNullOrEmpty(shop.Address))
            {
                item.Address = shop.Address;
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }
        [HttpDelete]
        public IActionResult DeleteShop(int id)
        {
            var item = _db.ShopDataModel.AsNoTracking().FirstOrDefault(x => x.ShopId == id);

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
