﻿// <auto-generated />
using System;
using E_CommerceOrderModule.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E_CommerceOrderModule.Repository.Migrations
{
    [DbContext(typeof(ECommerceOrderModuleContext))]
    [Migration("20220209214146_ECOrderModuleDB")]
    partial class ECOrderModuleDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("E_CommerceOrderModule.Core.Entity.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BasketId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("E_CommerceOrderModule.Core.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BrandName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrencyType")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("KDV")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MarketPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandName = "Apple",
                            CategoryName = "Cep Telefonu",
                            CurrencyType = 0,
                            Description = "P100AIPRO Iphone 11 PRO",
                            Image = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-shopping-carts/img1.webp",
                            KDV = 1m,
                            MarketPrice = 100m,
                            Name = "Iphone 11 PRO",
                            ProductId = "P100AIPRO",
                            SalePrice = 100m,
                            ShortDescription = "P100AIPRO Apple Iphone 11 PRO",
                            Status = 3,
                            Stock = 100,
                            UpdateDate = new DateTime(2022, 2, 10, 0, 41, 46, 40, DateTimeKind.Local).AddTicks(8588),
                            UploadDate = new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(272)
                        },
                        new
                        {
                            Id = 2,
                            BrandName = "Samsung",
                            CategoryName = "Cep Telefonu",
                            CurrencyType = 0,
                            Description = "P200SGN10 Samsung Galaxy Note 10",
                            Image = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-shopping-carts/img2.webp",
                            KDV = 1m,
                            MarketPrice = 200m,
                            Name = "Samsung Galaxy Note 10",
                            ProductId = "P200SGN10",
                            SalePrice = 200m,
                            ShortDescription = "P200SGN10 Samsung Galaxy Note 10",
                            Status = 3,
                            Stock = 100,
                            UpdateDate = new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8738),
                            UploadDate = new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8756)
                        },
                        new
                        {
                            Id = 3,
                            BrandName = "Canon",
                            CategoryName = "Kamera",
                            CurrencyType = 0,
                            Description = "P300CEM Canon EOS M50",
                            Image = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-shopping-carts/img3.webp",
                            KDV = 1m,
                            MarketPrice = 300m,
                            Name = "Canon EOS M50",
                            ProductId = "P300CEM",
                            SalePrice = 300m,
                            ShortDescription = "P300CEM Canon EOS M50",
                            Status = 3,
                            Stock = 100,
                            UpdateDate = new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8821),
                            UploadDate = new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8826)
                        },
                        new
                        {
                            Id = 4,
                            BrandName = "Apple",
                            CategoryName = "Bilgisayar",
                            CurrencyType = 0,
                            Description = "P300MBPRO MacBook Pro",
                            Image = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-shopping-carts/img4.webp",
                            KDV = 1m,
                            MarketPrice = 400m,
                            Name = "MacBook Pro",
                            ProductId = "P300MBPRO",
                            SalePrice = 400m,
                            ShortDescription = "P300MBPRO MacBook Pro",
                            Status = 3,
                            Stock = 100,
                            UpdateDate = new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8829),
                            UploadDate = new DateTime(2022, 2, 10, 0, 41, 46, 42, DateTimeKind.Local).AddTicks(8832)
                        });
                });

            modelBuilder.Entity("E_CommerceOrderModule.Core.Entity.Sales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsLog")
                        .HasColumnType("bit");

                    b.Property<string>("OrderNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalQuantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("E_CommerceOrderModule.Core.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
