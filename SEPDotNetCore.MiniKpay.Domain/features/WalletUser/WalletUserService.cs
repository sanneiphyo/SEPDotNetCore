using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEPDotNetCore.MiniKpay.DataBase.AppDbContextModels;


namespace SEPDotNetCore.MiniKpay.Domain.features.WalletUser
{
    public class WalletUserService
    {
        private readonly AppDbContext _db;

        public WalletUserService(AppDbContext context)
        {
            _db = context;
        }


    }
}
