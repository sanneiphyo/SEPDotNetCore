using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.MiniKpay.Domain.Features.WalletUser;
using SEPDotNetCore.MiniKpay.Domain.Models;

namespace SEPDotNetCore.MiniKpay.Api.Endpoints.WalletUser
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletUserController : ControllerBase
    {
        private readonly WalletUserService _service;

        public WalletUserController(WalletUserService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(TblWalletUser newUser)
        {
            try
            {
                var user = await _service.Register(newUser);
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _service.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
