using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SEPDotNetCore.Database.Models;

public partial class AppDbContext : DbContext
{
  
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        string connectionString = "Server=.;Database=SEPDotNetCore;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";
    //        optionsBuilder.UseSqlServer(connectionString);
    //    }
    //}

 //"Data Source=.;Initial Catalog=SEPDotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(10);
            entity.Property(e => e.BlogTitle).HasMaxLength(10);
            entity.Property(e => e.DeleteFlag)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
