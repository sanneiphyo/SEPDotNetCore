using Microsoft.AspNetCore.Mvc;
using SEPDotNetCore.MiniKpay.DataBase.AppDbContextModels;
using SEPDotNetCore.MiniKpay.Domain.features.WalletUser;


namespace SEPDotNetCore.MiniKpay.Api.Endpoints.WalletUser
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletUserController : ControllerBase
    {
        private readonly UserService _service;

        public WalletUserController(UserService service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public IActionResult Register(TblWalletUser newUser)
        {
            var user = _service.Register(newUser);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _service.GetUser(id);

            return Ok(user);
        }


        [HttpPatch("UpdateProfile/{id}")]
        public IActionResult UpdateProfile(int id,TblWalletUser updatedUser)
        {
            var user = _service.UpdateProfile(id, updatedUser);
            return Ok(user);
        }


        [HttpPatch("ChangePin/{id}")]
        public IActionResult ChangePin(int id, TblWalletUser newPin)
        {
            var user = _service.ChangePin(id, newPin);
            return Ok(user);
        }
    }
}
