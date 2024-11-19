using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SEPDotNetCore.MiniKpay.Domain.Models
{
    public class TblWalletUser

    {
        public int UserId {  get; set; }
        public string MobileNumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Pincode { get; set; } = null!;
        public decimal? Balance { get; set; }
        public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
    }
}
