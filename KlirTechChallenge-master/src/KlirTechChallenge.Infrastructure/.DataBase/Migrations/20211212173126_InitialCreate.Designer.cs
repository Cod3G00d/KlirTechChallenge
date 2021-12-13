﻿// <auto-generated />
using System;
using KlirTechChallenge.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KlirTechChallenge.Infrastructure.DataBase.Migrations
{
    [DbContext(typeof(KlirTechChallengeContext))]
    [Migration("20211212173126_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KlirTechChallenge.Domain.Core.Events.StoredEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Payload")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProcessedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("StoredEvents", "dbo");
                });

            modelBuilder.Entity("KlirTechChallenge.Domain.Customers.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Customers", "dbo");
                });

            modelBuilder.Entity("KlirTechChallenge.Domain.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("QuoteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("StatusId");

                    b.HasKey("Id");

                    b.ToTable("Orders", "dbo");
                });

            modelBuilder.Entity("KlirTechChallenge.Domain.Payments.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("PaidAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("PaidAt");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("StatusId");

                    b.HasKey("Id");

                    b.ToTable("Payments", "dbo");
                });

            modelBuilder.Entity("KlirTechChallenge.Domain.Products.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<Guid?>("PromotionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Products", "dbo");
                });

            modelBuilder.Entity("KlirTechChallenge.Domain.Promotions.Promotion", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("Promotions", "dbo");
                });

            modelBuilder.Entity("KlirTechChallenge.Domain.Quotes.Quote", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Quotes", "dbo");
                });

            modelBuilder.Entity("KlirTechChallenge.Domain.Orders.Order", b =>
                {
                    b.OwnsOne("KlirTechChallenge.Domain.SharedKernel.Money", "TotalPrice", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CurrencyCode")
                                .HasMaxLength(5)
                                .HasColumnType("nvarchar(5)")
                                .HasColumnName("Currency");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(5,2)")
                                .HasColumnName("TotalPrice");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsMany("KlirTechChallenge.Domain.Orders.OrderLine", "OrderLines", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.HasKey("OrderId", "ProductId");

                            b1.ToTable("OrderLines", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.OwnsOne("KlirTechChallenge.Domain.SharedKernel.Money", "ProductBasePrice", b2 =>
                                {
                                    b2.Property<Guid>("OrderLineOrderId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("OrderLineProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("CurrencyCode")
                                        .HasMaxLength(5)
                                        .HasColumnType("nvarchar(5)")
                                        .HasColumnName("BaseCurrency");

                                    b2.Property<decimal>("Value")
                                        .HasColumnType("decimal(5,2)")
                                        .HasColumnName("BasePrice");

                                    b2.HasKey("OrderLineOrderId", "OrderLineProductId");

                                    b2.ToTable("OrderLines", "dbo");

                                    b2.WithOwner()
                                        .HasForeignKey("OrderLineOrderId", "OrderLineProductId");
                                });

                            b1.OwnsOne("KlirTechChallenge.Domain.SharedKernel.Money", "ProductExchangePrice", b2 =>
                                {
                                    b2.Property<Guid>("OrderLineOrderId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<Guid>("OrderLineProductId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("CurrencyCode")
                                        .HasMaxLength(5)
                                        .HasColumnType("nvarchar(5)")
                                        .HasColumnName("ExchangeCurrency");

                                    b2.Property<decimal>("Value")
                                        .HasColumnType("decimal(5,2)")
                                        .HasColumnName("ExchangePrice");

                                    b2.HasKey("OrderLineOrderId", "OrderLineProductId");

                                    b2.ToTable("OrderLines", "dbo");

                                    b2.WithOwner()
                                        .HasForeignKey("OrderLineOrderId", "OrderLineProductId");
                                });

                            b1.Navigation("ProductBasePrice");

                            b1.Navigation("ProductExchangePrice");
                        });

                    b.Navigation("OrderLines");

                    b.Navigation("TotalPrice");
                });

            modelBuilder.Entity("KlirTechChallenge.Domain.Products.Product", b =>
                {
                    b.OwnsOne("KlirTechChallenge.Domain.SharedKernel.Money", "Price", b1 =>
                        {
                            b1.Property<Guid>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("CurrencyCode")
                                .IsRequired()
                                .HasMaxLength(5)
                                .HasColumnType("nvarchar(5)")
                                .HasColumnName("Currency");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(5,2)")
                                .HasColumnName("Price");

                            b1.HasKey("ProductId");

                            b1.ToTable("Products", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("ProductId");
                        });

                    b.Navigation("Price");
                });

            modelBuilder.Entity("KlirTechChallenge.Domain.Quotes.Quote", b =>
                {
                    b.OwnsMany("KlirTechChallenge.Domain.Quotes.QuoteItem", "Items", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid?>("ProductId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.Property<Guid>("QuoteId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<double>("TotalPrice")
                                .HasColumnType("float");

                            b1.HasKey("Id");

                            b1.HasIndex("QuoteId");

                            b1.ToTable("QuoteItems", "dbo");

                            b1.WithOwner()
                                .HasForeignKey("QuoteId");
                        });

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}