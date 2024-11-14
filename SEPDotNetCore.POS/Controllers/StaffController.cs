using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;
namespace SEPDotNetCore.POS.Controllers
{
    namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();


        [HttpGet]
        public IActionResult GetShops()
        {
            var lst = _db.StaffDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetShop(int id)
        {
            var item = _db.StaffDataModel.AsNoTracking().FirstOrDefault(x => x.StaffId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateStaff(StaffDataModel staff)
        {
            _db.StaffDataModel.Add(staff);
            _db.SaveChanges();
            return Ok(new { Message = "Create Staff Data" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStaff(int id, StaffDataModel staff)
        {
            var item = _db.StaffDataModel.AsNoTracking().FirstOrDefault(x => x.StaffId == id);

            if (item is null)
            {
                return NotFound();
            }

            item.StaffCode = staff.StaffCode;
            item.StaffName = staff.StaffName;
            item.DateOfBirth = staff.DateOfBirth;
            item.MobileNo = staff.MobileNo;
            item.Address = staff.Address;
            item.Gender = staff.Gender;
            item.Position = staff.Position;
            item.Password = staff.Password;

                _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

        [HttpPatch("{id}")]
        public IActionResult PatchStaff(int id, StaffDataModel staff)
        {
            var item = _db.StaffDataModel.AsNoTracking().FirstOrDefault(x => x.StaffId == id);

            if (item is null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(staff.StaffCode))
            {
                    item.StaffCode = staff.StaffCode;
             }
            if (!string.IsNullOrEmpty(staff.StaffName))
            {
                    item.StaffName = staff.StaffName;
            }
         
            if (!string.IsNullOrEmpty(staff.MobileNo))
            {
                    item.MobileNo = staff.MobileNo;
            }
            if (!string.IsNullOrEmpty(staff.Address))
             {
                    item.Address = staff.Address;
            }
             if (!string.IsNullOrEmpty(staff.Gender))
             {
                    item.Gender = staff.Gender;
             }
             if (!string.IsNullOrEmpty(staff.Position))
             {
                    item.Position = staff.Position;
             }
             if (!string.IsNullOrEmpty(staff.Password))
             {
                    item.Password = staff.Password;
             }
                    _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);
        }
        [HttpDelete]
        public IActionResult DeleteStaff(int id)
        {
            var item = _db.StaffDataModel.AsNoTracking().FirstOrDefault(x => x.StaffId == id);

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
