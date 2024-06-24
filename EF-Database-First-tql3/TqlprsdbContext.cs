﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EF_Database_First_tql3;

public partial class TqlprsdbContext : DbContext
{
    public TqlprsdbContext()
    {
    }

    public TqlprsdbContext(DbContextOptions<TqlprsdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<Requestline> Requestlines { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost\\sqlexpress;database=tqlprsdb;trusted_connection=true;trustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC071F332B70");

            entity.HasIndex(e => e.PartNbr, "UQ__Products__DAFC0C1EC000F589").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PartNbr).HasMaxLength(30);
            entity.Property(e => e.PhotoPath).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.Unit).HasMaxLength(30);

            entity.HasOne(d => d.Vendor).WithMany(p => p.Products)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Vendor__403A8C7D");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requests__3214EC07D668977C");

            entity.Property(e => e.DeliveryMode).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(80);
            entity.Property(e => e.Justification).HasMaxLength(80);
            entity.Property(e => e.RejectionReason).HasMaxLength(80);
            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.Total).HasColumnType("decimal(11, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.Requests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__UserId__4316F928");
        });

        modelBuilder.Entity<Requestline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requestl__3214EC07BA38547F");

            entity.HasOne(d => d.Product).WithMany(p => p.Requestlines)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requestli__Produ__46E78A0C");

            entity.HasOne(d => d.Request).WithMany(p => p.Requestlines)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requestli__Reque__45F365D3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07B7EFF00B");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4EE0EB37D").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Firstname).HasMaxLength(30);
            entity.Property(e => e.Lastname).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.Username).HasMaxLength(30);
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vendors__3214EC07C46AF3F0");

            entity.HasIndex(e => e.Code, "UQ__Vendors__A25C5AA7803FECD1").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(30);
            entity.Property(e => e.City).HasMaxLength(30);
            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.State).HasMaxLength(2);
            entity.Property(e => e.Zip).HasMaxLength(5);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
