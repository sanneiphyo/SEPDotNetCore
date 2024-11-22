using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.MiniKpay.Domain.Models
{
      public class Result<T> //Response ပြန်တိုင်း <T> ကပြန်ငုံမယ် 
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }
        public bool IsValidationError { get { return Type == EnumRespType.ValidationError; } }
        public bool IsSystemError { get { return Type == EnumRespType.SystemError; } }
        private EnumRespType Type { get; set; }
        public T Data { get; set; }//အခြား Model တွေမှာ BaseResponseModel(child) ကို တစ်ဆင့်ခေါ်ပြီးမှ သုံးတယ် ိိိိူုဒီမှာတော့ မသိကိန်း ထားထားတဲ့  T  ထဲ Data  တိုက်ရိုက်ဝင်တယ်
        public string Message { get; set; }

        public static Result<T> Success(T data, string message = "Success.")
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Type = EnumRespType.Success,
                Data = data,
                Message = message
            };
        }

        public static Result<T> ValidationError(string message, T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message,
                Type = EnumRespType.ValidationError,
            };
        }

        public static Result<T> SystemError(string message, T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Data = data,
                Message = message,
                Type = EnumRespType.SystemError,
            };
        }
    }
}
