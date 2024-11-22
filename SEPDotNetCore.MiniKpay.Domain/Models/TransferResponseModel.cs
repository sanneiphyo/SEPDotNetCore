using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.MiniKpay.Domain.Models
{
    //BaseResponseModel ကိုတစ်ဆင့်သုံးထားကာ transferResponseModel တည်ဆောက်ထားခြင်းဖြစ်သည်
    public class TransferResponseModel
    {
        public BaseResponseModel responseModel { get; set; }

    }
}
