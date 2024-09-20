using Microsoft.EntityFrameworkCore;
using SEPDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.ConsoleApp
{
    internal class AppDbContext : DbContext
    {
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source =.;Initial Catalog=SEPDotNetCore;User ID =sa;Password=sasa@123; TrustServerCertificate=True ";
                optionsBuilder.UseSqlServer();
            }
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
