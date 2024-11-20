using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.MiniKpay.DataBase.DataBase.AppDbContextModels;
using SEPDotNetCore.MiniKpay.Domain.features.WalletUser;


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

        [HttpPost("Register")]
        public IActionResult Register([FromBody] TblWalletUser newUser)
        {
            
            try
            {
                var user =  _service.Register(newUser);
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _service.GetUser(id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            return Ok(user);
        }


        [HttpPatch("UpdateProfile/{id}")]
        public Task<IActionResult> UpdateProfile(int id, [FromBody] TblWalletUser updatedUser)
        {
            try
            {
                var user =  _service.UpdateProfile(id, updatedUser);
                if (user == null)
                {
                    return Task.FromResult<IActionResult>(NotFound());
                }
                return Task.FromResult<IActionResult>(NoContent());
            }
            catch (ArgumentException ex)
            {
                return Task.FromResult<IActionResult>(BadRequest(new { message = ex.Message }));
            }
        }
    }
}
