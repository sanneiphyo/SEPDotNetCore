using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.MiniKpay.DataBase.AppDbContextModels;

using SEPDotNetCore.MiniKpay.Domain.features.User;
using SEPDotNetCore.MiniKpay.Domain.Models;


namespace SEPDotNetCore.MiniKpay.Api.Endpoints.WalletUser
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public  async Task<IActionResult> Register(TblWalletUser newUser)
        {
            var user =await _service.Register(newUser);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            var user = await _service.GetUser(id);

            return Ok(user);
        }


        [HttpPatch("UpdateProfile/{id}")]
        public async Task<IActionResult> UpdateProfileAsync(int id,TblWalletUser updatedUser)
        {
            var user = await _service.UpdateProfileAsync(id, updatedUser);
            return Ok(user);
        }


      
        [HttpPatch("ChangePin/{id}")]
        public async Task<IActionResult> ChangePin(int id, TblWalletUser newPin)
        {
            var user =await  _service.ChangePin(id, newPin);
            return Ok(user);
        }
    }
}
