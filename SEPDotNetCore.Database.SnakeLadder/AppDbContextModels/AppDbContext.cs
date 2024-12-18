using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SEPDotNetCore.SnakeLadder.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBoard> TblBoards { get; set; }

    public virtual DbSet<TblGame> TblGames { get; set; }

    public virtual DbSet<TblGamePlay> TblGamePlays { get; set; }

    public virtual DbSet<TblPlayer> TblPlayers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=SankeLadderGame;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBoard>(entity =>
        {
            entity.HasKey(e => e.BoardId).HasName("PK__Tbl_Boar__F9646BD2C1A0DDDF");

            entity.ToTable("Tbl_Board");

            entity.HasIndex(e => e.BoardNumber, "UQ__Tbl_Boar__EA937B3661D168F3").IsUnique();

            entity.Property(e => e.BoardId).HasColumnName("BoardID");
        });

        modelBuilder.Entity<TblGame>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Tbl_Game__2AB897DD17674DD1");

            entity.ToTable("Tbl_Games");

            entity.HasIndex(e => e.GameCode, "UQ__Tbl_Game__18C8460C55BF89C8").IsUnique();

            entity.Property(e => e.GameCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.WinnerPlayer).WithMany(p => p.TblGames)
                .HasForeignKey(d => d.WinnerPlayerId)
                .HasConstraintName("FK__Tbl_Games__Winne__4CA06362");
        });

        modelBuilder.Entity<TblGamePlay>(entity =>
        {
            entity.HasKey(e => e.MoveId).HasName("PK__Tbl_Game__A931A43C688F17BB");

            entity.ToTable("Tbl_GamePlay");

            entity.Property(e => e.GameCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.NewPositionNavigation).WithMany(p => p.TblGamePlays)
                .HasPrincipalKey(p => p.BoardNumber)
                .HasForeignKey(d => d.NewPosition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_GameP__NewPo__5070F446");

            entity.HasOne(d => d.Player).WithMany(p => p.TblGamePlays)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_GameP__Playe__4F7CD00D");
        });

        modelBuilder.Entity<TblPlayer>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Tbl_Play__4A4E74A80D68482C");

            entity.ToTable("Tbl_Players");

            entity.Property(e => e.CurrentPosition).HasDefaultValueSql("((1))");
            entity.Property(e => e.PlayerName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CurrentPositionNavigation).WithMany(p => p.TblPlayers)
                .HasPrincipalKey(p => p.BoardNumber)
                .HasForeignKey(d => d.CurrentPosition)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Playe__Curre__48CFD27E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
