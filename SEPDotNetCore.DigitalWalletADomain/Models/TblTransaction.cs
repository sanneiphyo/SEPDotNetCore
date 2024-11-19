using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SEPDotNetCore.MiniKpay.Domain.Models
{
    public class TblTransaction
    {
        public int TransactionId { get; set; }
        public int SenderUserId { get; set; }
        public int ReciverUserId { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public virtual TblWalletUser? SenderUser { get; set; }
    }
}
