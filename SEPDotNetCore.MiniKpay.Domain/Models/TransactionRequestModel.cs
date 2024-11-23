using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.MiniKpay.Domain.Models
{
    public class TransactionRequestModel
    {

        public int UserId { get; set; }
      
        public decimal Amount { get; set; }
    }
}
