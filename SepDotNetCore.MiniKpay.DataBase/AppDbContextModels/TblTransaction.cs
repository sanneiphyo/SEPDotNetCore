using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SepDotNetCore.MiniKpay.DataBase.AppDbContextModel
{
    public class TblTransaction
    {
        public int TransactionId { get; set; }

        public int? SenderUserId { get; set; }

        public int? ReceiverUserId { get; set; }

        public string? TransactionType { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? TransactionDate { get; set; }

        public virtual TblWalletUser? SenderUser { get; set; }
    }
}
