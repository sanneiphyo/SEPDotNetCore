using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static SEPDotNetCore.POS.Model.POSDataModel;
namespace SEPDotNetCore.POS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetExpenses()
        {
            var lst = _db.ExpenseDataModel.AsNoTracking().Where(x => x.DeleteFlag == false).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetExpense(int id)
        {
            var item = _db.ExpenseDataModel.AsNoTracking().FirstOrDefault(x => x.ExpenseId == id);

            if (item is null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreatePayment(ExpenseDataModel expense)
        {
            _db.ExpenseDataModel.Add(expense);
            _db.SaveChanges();
            return Ok(new { Message = "Create Expense" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExpense(int id, ExpenseDataModel expense)
        {
            var item = _db.ExpenseDataModel.AsNoTracking().FirstOrDefault(x => x.ExpenseId == id);

            if (item is null)
            {
                return NotFound();
            }
            item.Description= expense.Description;
            item.Amount= expense.Amount;
            item.Date = expense.Date;


            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(item);

        }

        [HttpDelete]
        public IActionResult DeleteExpense(int id)
        {
            var item = _db.ExpenseDataModel.AsNoTracking().FirstOrDefault(x => x.ExpenseId == id);


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
