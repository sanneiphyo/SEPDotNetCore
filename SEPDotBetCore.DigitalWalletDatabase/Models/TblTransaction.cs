using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotBetCore.DigitalWalletDatabase.AppDbContextModels
{
    public class TblTransaction
    {

        public int TransctionId { get; set; }
        public int SenderUserId { get; set; }
        public int ReciverUserId { get; set; }
        public string? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransctionDate{ get; set;}
        public virtual TblWalletUser? SenderUser { get; set; }
    }
}
