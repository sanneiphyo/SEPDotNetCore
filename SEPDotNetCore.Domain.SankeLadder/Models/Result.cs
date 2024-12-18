using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.SankeLadder.Domain.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }

        private EnumRespType Type { get; set; }

        public bool IsValidationError { get { return Type == EnumRespType.ValidationError; } }
        public bool IsSystemError { get { return Type == EnumRespType.SystemError; } }

        public T data { get; set; }
        public string Message { get; set; }


        public static Result<T> Success(T data, string message = "Success ")
        {
            return new Result<T>
            {
                IsSuccess = true,
                Type = EnumRespType.Success,
                Message = message

            };
        }

        public static Result<T> ValidationError(T? data = default, string message = "Validation Error")
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumRespType.ValidationError,
                Message = message
            };
        }

        public static Result<T> SystemError (T? data = default ,string message = "System Error")
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumRespType.SystemError,
                Message = message
            };
        }

        public enum EnumRespType
        {
            None,
            Success,
            ValidationError,
            SystemError,

        }
    }
};
