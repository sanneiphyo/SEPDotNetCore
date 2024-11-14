using Microsoft.EntityFrameworkCore;
using SEPDotNetCore.POS.Model;
using static SEPDotNetCore.POS.Model.POSDataModel;

namespace SEPDotNetCore.POS
{
    public class AppDbContext : DbContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    string connectionString = "Data Source=.;Initial Catalog=POSDotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }

          
            public DbSet<ProductDataModel> ProductDataModel { get; set; }
            public DbSet<ProductCategoryDataModel> ProductCategoryDataModel { get; set; }
            public DbSet<ShopDataModel>ShopDataModel { get; set; }

    }
}
