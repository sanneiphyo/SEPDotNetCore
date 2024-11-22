using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEPDotNetCore.MiniKpay.DataBase.AppDbContextModels;

namespace SEPDotNetCore.MiniKpay.Domain.Models
{
    
    public class TransferResponseModel
    {
        //BaseResponseModel ကိုတစ်ဆင့်သုံးထားကာ transferResponseModel တည်ဆောက်ထားခြင်းဖြစ်သည်
        public BaseResponseModel responseModel { get; set; }

        public TblTransaction   Transaction { get; set; }

    }

    public class ResultTransferResponseModel
    {
      
        public TblTransaction Transaction { get; set; }

    }
}
