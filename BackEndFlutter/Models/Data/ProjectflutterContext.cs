﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BackEndFlutter.Models.Data
{
    public partial class ProjectflutterContext : DbContext
    {
        public ProjectflutterContext()
        {
        }

        public ProjectflutterContext(DbContextOptions<ProjectflutterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<CategoryFood> CategoryFood { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Details> Details { get; set; }
        public virtual DbSet<FoodOptions> FoodOptions { get; set; }
        public virtual DbSet<Foods> Foods { get; set; }
        public virtual DbSet<ImageFood> ImageFood { get; set; }
        public virtual DbSet<OderDetail> OderDetail { get; set; }
        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<OptionsDetail> OptionsDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrdersDelivery> OrdersDelivery { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<StatusDelivery> StatusDelivery { get; set; }
        public virtual DbSet<StatusOrder> StatusOrder { get; set; }
        public virtual DbSet<Tables> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_100_CI_AI");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoryFood>(entity =>
            {
                entity.Property(e => e.TypeFood)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CusId);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("telephone");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Details>(entity =>
            {
                entity.HasKey(e => e.DetailId);

                entity.Property(e => e.DetailId).HasColumnName("detail_Id");

                entity.Property(e => e.OptionsDetailId).HasColumnName("optionsDetailId");

                entity.Property(e => e.OptionsId).HasColumnName("optionsId");

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetail_Id");

                entity.HasOne(d => d.OptionsDetail)
                    .WithMany(p => p.Details)
                    .HasForeignKey(d => d.OptionsDetailId)
                    .HasConstraintName("FK_Details_OptionsDetail");

                entity.HasOne(d => d.Options)
                    .WithMany(p => p.Details)
                    .HasForeignKey(d => d.OptionsId)
                    .HasConstraintName("FK_Details_Options");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.DetailsNavigation)
                    .HasForeignKey(d => d.OrderDetailId)
                    .HasConstraintName("FK_Details_OderDetail");
            });

            modelBuilder.Entity<FoodOptions>(entity =>
            {
                entity.HasKey(e => e.FoodoptionId);

                entity.Property(e => e.OptionsId).HasColumnName("optionsId");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.FoodOptions)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK_FoodOptions_Foods");

                entity.HasOne(d => d.Options)
                    .WithMany(p => p.FoodOptions)
                    .HasForeignKey(d => d.OptionsId)
                    .HasConstraintName("FK_FoodOptions_Options");
            });

            modelBuilder.Entity<Foods>(entity =>
            {
                entity.HasKey(e => e.FoodId);

                entity.Property(e => e.CatefoodId).HasColumnName("catefoodId");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Catefood)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.CatefoodId)
                    .HasConstraintName("FK_Foods_CategoryFood");
            });

            modelBuilder.Entity<ImageFood>(entity =>
            {
                entity.HasKey(e => e.ImageId);

                entity.Property(e => e.Path).IsUnicode(false);

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.ImageFood)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK_ImageFood_Foods");
            });

            modelBuilder.Entity<OderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderdetailId);

                entity.Property(e => e.OrderdetailId).HasColumnName("orderdetailId");

                entity.Property(e => e.Details)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("details");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.OrderdeliveryId).HasColumnName("orderdeliveryId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.Food)
                    .WithMany(p => p.OderDetail)
                    .HasForeignKey(d => d.FoodId)
                    .HasConstraintName("FK_OderDetail_Foods");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OderDetail_Orders");

                entity.HasOne(d => d.Orderdelivery)
                    .WithMany(p => p.OderDetail)
                    .HasForeignKey(d => d.OrderdeliveryId)
                    .HasConstraintName("FK_OderDetail_OrdersDelivery");
            });

            modelBuilder.Entity<Options>(entity =>
            {
                entity.Property(e => e.OptionsId).HasColumnName("optionsId");

                entity.Property(e => e.Titlename)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OptionsDetail>(entity =>
            {
                entity.Property(e => e.OptionsDetailId).HasColumnName("optionsDetailId");

                entity.Property(e => e.OptionsId).HasColumnName("optionsId");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Typename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("typename");

                entity.HasOne(d => d.Options)
                    .WithMany(p => p.OptionsDetail)
                    .HasForeignKey(d => d.OptionsId)
                    .HasConstraintName("FK_OptionsDetail_Options");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.DateIn)
                    .HasColumnType("date")
                    .HasColumnName("dateIn");

                entity.Property(e => e.StatusId).HasColumnName("statusId");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Orders_StatusOrder");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TableId)
                    .HasConstraintName("FK_Orders_Tables");
            });

            modelBuilder.Entity<OrdersDelivery>(entity =>
            {
                entity.HasKey(e => e.OrderDeliveryId);

                entity.Property(e => e.DateIn)
                    .HasColumnType("date")
                    .HasColumnName("dateIn");

                entity.Property(e => e.Details)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("details");

                entity.Property(e => e.Lat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lat");

                entity.Property(e => e.Long)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("long");

                entity.Property(e => e.StatusdeliveryId).HasColumnName("statusdeliveryId");

                entity.HasOne(d => d.Cus)
                    .WithMany(p => p.OrdersDelivery)
                    .HasForeignKey(d => d.CusId)
                    .HasConstraintName("FK_OrdersDelivery_Customers");

                entity.HasOne(d => d.Statusdelivery)
                    .WithMany(p => p.OrdersDelivery)
                    .HasForeignKey(d => d.StatusdeliveryId)
                    .HasConstraintName("FK_OrdersDelivery_StatusDelivery");
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.Property(e => e.DateIn)
                    .HasColumnType("date")
                    .HasColumnName("dateIn");

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.OrderDelivery)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderDeliveryId)
                    .HasConstraintName("FK_Payments_OrdersDelivery");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Payments_Orders");
            });

            modelBuilder.Entity<StatusDelivery>(entity =>
            {
                entity.Property(e => e.StatusdeliveryId).HasColumnName("statusdeliveryId");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StatusOrder>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.Property(e => e.StatusId).HasColumnName("statusId");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Tables>(entity =>
            {
                entity.HasKey(e => e.TableId);

                entity.Property(e => e.Name)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}