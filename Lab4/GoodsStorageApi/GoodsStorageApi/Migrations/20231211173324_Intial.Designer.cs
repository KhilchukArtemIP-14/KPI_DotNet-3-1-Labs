﻿// <auto-generated />
using System;
using GoodsStorage.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GoodsStorage.API.Migrations
{
    [DbContext(typeof(GoodsStorageDbContext))]
    [Migration("20231211173324_Intial")]
    partial class Intial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GoodsStorage.DAL.Models.Domain.Good", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AvailableAmount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("GoodsStorage.DAL.Models.Domain.Purchase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("StaffRepId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("GoodsStorage.DAL.Models.Domain.PurchaseGood", b =>
                {
                    b.Property<Guid>("PurchaseId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(2);

                    b.Property<Guid>("GoodId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("PurchaseId", "GoodId");

                    b.HasIndex("GoodId");

                    b.ToTable("PurchaseGoods");
                });

            modelBuilder.Entity("GoodsStorage.DAL.Models.Domain.Request", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ExpectedPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("GoodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GoodId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("GoodsStorage.DAL.Models.Domain.PurchaseGood", b =>
                {
                    b.HasOne("GoodsStorage.DAL.Models.Domain.Good", "Good")
                        .WithMany("PurchaseGoods")
                        .HasForeignKey("GoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GoodsStorage.DAL.Models.Domain.Purchase", "Purchase")
                        .WithMany("PurchaseGoods")
                        .HasForeignKey("PurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Good");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("GoodsStorage.DAL.Models.Domain.Request", b =>
                {
                    b.HasOne("GoodsStorage.DAL.Models.Domain.Good", "Good")
                        .WithMany()
                        .HasForeignKey("GoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Good");
                });

            modelBuilder.Entity("GoodsStorage.DAL.Models.Domain.Good", b =>
                {
                    b.Navigation("PurchaseGoods");
                });

            modelBuilder.Entity("GoodsStorage.DAL.Models.Domain.Purchase", b =>
                {
                    b.Navigation("PurchaseGoods");
                });
#pragma warning restore 612, 618
        }
    }
}
