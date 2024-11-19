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
        private readonly WallerUserService _service;

        public WalletUserController(WallerUserService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(TblWalletUser newUser)
        {
            try
            {
                var user = await  _service.Register(newUser);
            }
            catch 
            {
              
            }
               
        
        }    
 }
}
