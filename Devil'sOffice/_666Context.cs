using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Devil_sOffice;

public partial class _666Context : DbContext
{
    public _666Context()
    {
    }

    public _666Context(DbContextOptions<_666Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Devil> Devils { get; set; }

    public virtual DbSet<Disposal> Disposals { get; set; }

    public virtual DbSet<Rack> Racks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=192.168.200.13;user=student;password=student;charset=utf8mb4;database=666", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.39-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Devil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("devil");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Nick)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("погоняло")
                .HasColumnName("nick");
            entity.Property(e => e.Rank)
                .HasComment("кол-во душ")
                .HasColumnType("int(11)")
                .HasColumnName("rank");
            entity.Property(e => e.Year)
                .HasColumnType("int(11)")
                .HasColumnName("year");
        });

        modelBuilder.Entity<Disposal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("disposal");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("current_timestamp()")
                .HasComment("дата утилизации")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("название объекта")
                .HasColumnName("title");
            entity.Property(e => e.Year)
                .HasComment("дата покупки")
                .HasColumnType("int(11)")
                .HasColumnName("year");
        });

        modelBuilder.Entity<Rack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rack");

            entity.HasIndex(e => e.IdDevil, "FK_rack_id_devil");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.CurrentCount)
                .HasComment("кол-во применений")
                .HasColumnType("int(11)")
                .HasColumnName("current_count");
            entity.Property(e => e.IdDevil)
                .HasColumnType("int(11)")
                .HasColumnName("id_devil");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasComment("название \"стеллажа\"")
                .HasColumnName("title");
            entity.Property(e => e.UseCount)
                .HasComment("макс кол-во применений")
                .HasColumnType("int(11)")
                .HasColumnName("use_count");
            entity.Property(e => e.YearBuy)
                .HasColumnType("int(11)")
                .HasColumnName("year_buy");

            entity.HasOne(d => d.IdDevilNavigation).WithMany(p => p.Racks)
                .HasForeignKey(d => d.IdDevil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_rack_id_devil");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
