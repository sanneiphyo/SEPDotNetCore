using System;
using System.Collections.Generic;

namespace SEPDotNetCore.MiniKpay.DataBase.AppDbContextModels;

public partial class TblTransaction
{
    public int TransactionId { get; set; }

    public int SenderUserId { get; set; }

    public int ReceiverUserId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public virtual TblWalletUser? SenderUser { get; set; }

}
