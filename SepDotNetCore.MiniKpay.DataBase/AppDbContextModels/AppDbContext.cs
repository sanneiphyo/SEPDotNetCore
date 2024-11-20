using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SepDotNetCore.MiniKpay.DataBase.AppDbContextModel;

namespace SEPDotNetCore.MiniKpay.DataBase.AppDbContextModels
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)

            {
                string connectionString = "Data Source=.;Initial Catalog=DigitalWallet;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
           
    }
        public DbSet<TblWalletUser>? User { get; set; }
        public DbSet<TblTransaction>? Transactions { get; set; }
    }
}
