using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SepDotNetCore.MiniKpay.DataBase.AppDbContextModel
{
    public class TblWalletUser
    {
        public int UserId { get; set; }

        public string MobileNumber { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public string PinCode { get; set; } = null!;

        public decimal? Balance { get; set; }

        public string? Status { get; set; }

        public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
    }
}
