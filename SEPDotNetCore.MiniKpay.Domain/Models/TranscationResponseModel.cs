using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.MiniKpay.Domain.Models
{
    public class TransactionResponseModel
    {

        public class ResultTransactionResponseModel
        {

            public TblTransaction Transaction { get; set; }

        }
    }
}
