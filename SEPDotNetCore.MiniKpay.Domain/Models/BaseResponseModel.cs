using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.MiniKpay.Domain.Models
{
    //BaseResponseModel သည်အခြားသော Model များ၏ base model အဖြစ်သတ်မှတ်နိုင် 
    public class BaseResponseModel
    {
        public string RespCode { get; set; }
        public string RespDesp { get; set; }
        public EnumRespType RespType { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }

        //Response Code များကိုလွယ်ကူစွာ ခေါ်ယူအသုံးပြုနိုင်စေရန် ရေးသားထားခြင်းဖြစ်သည်
        public static BaseResponseModel Success(string respCode, string respDesp)
        {
            return new BaseResponseModel()
            {
                IsSuccess = true,
                RespCode  = respCode,
                RespDesp = respDesp,
                RespType = EnumRespType.Success,
            };
        }

        public static BaseResponseModel ValidationError(string respCode, string respDesp)
        {
            return new BaseResponseModel()
            {
                IsSuccess = true,
                RespCode = respCode,
                RespDesp = respDesp,
                RespType = EnumRespType.ValidationError,
            };
        }

        public static BaseResponseModel SystemError(string respCode, string respDesp)
        {
            return new BaseResponseModel()
            {
                IsSuccess = true,
                RespCode = respCode,
                RespDesp = respDesp,
                RespType = EnumRespType.SystemError,
            };
        }
    }

    public enum EnumRespType
    {
        None, //Enum တွင် client ဘက်မှ ိReq data ဘာမှမထည့်ပေးလိုက်သော် အပေါ်မှာ None မသုံးထားလျှင် Success ဖြသ်သွားနိုင်သဖြင့်( None)အသုံးပြုထားခြင်းဖြစ်သည်
        Success,
        ValidationError,
        SystemError

    }
}
