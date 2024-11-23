using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SEPDotNetCore.MiniKpay.Domain.Models;

namespace SEPDotNetCore.MiniKpay.Api.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class baseController : ControllerBase
    {

        public IActionResult Execute(object model) //model သည် obj ဖြစ်သောကြောင့် Response ဆိုတာ ကိုမသိနိုင် 

            //obj နဲ့့ဆို ဘာမဆိုလက်ခံတယ် ဒါကြောင့် ဲျJsonConvert = stringအရင်ပြောငိးပေးတယ် 
        {
            JObject jObj = JObject.Parse(JsonConvert.SerializeObject(model)); //string ပြောင်းပြီး JObject နဲ့ ပြန်ငုံရေးထားခြင်းဖြစ်တယ်
            if (jObj.ContainsKey("responseModel")) 
            {
                BaseResponseModel baseResponseModel = JsonConvert.DeserializeObject<BaseResponseModel>(
                        jObj["responseModel"]!.ToString()!)!;

                if (baseResponseModel.RespType == EnumRespType.pending)
                    return BadRequest(model);

                if (baseResponseModel.RespType == EnumRespType.ValidationError)
                    return BadRequest(model);

                if (baseResponseModel.RespType == EnumRespType.SystemError)
                    return StatusCode(500, model);

                return Ok(model);
                //ရည်ရွယ်ချက်မှာ obj ဆိုရင် ုKey စစ်မရလို့ ဲ Json ပြောင်း စစ်ပြီး မှ Obj ပြန်ပြောင်းထားခြင်းဖြစ်တယ်
            }
            return StatusCode(500, "Invalid Response Model. Please add BaseResponseModel to your ResponseModel.");
        }
    }
}
8