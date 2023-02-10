﻿// <auto-generated />
using System;
using EasyTrade.DAL.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EasyTrade.DAL.Migrations
{
    [DbContext(typeof(EasyTradeDbContext))]
    partial class EasyTradeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EasyTrade.DAL.Model.TradeCoefficient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Coefficient")
                        .HasColumnType("numeric");

                    b.Property<int>("Operation")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("coefficients");
                });

            modelBuilder.Entity("EasyTrade.DTO.Model.Balance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<long>("CurrencyId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.ToTable("balances");
                });

            modelBuilder.Entity("EasyTrade.DTO.Model.BrokerCurrencyTrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("BuyAmount")
                        .HasColumnType("numeric");

                    b.Property<long>("BuyCcyId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("SellAmount")
                        .HasColumnType("numeric");

                    b.Property<long>("SellCcyId")
                        .HasColumnType("bigint");

                    b.Property<int>("TradeType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BuyCcyId");

                    b.HasIndex("SellCcyId");

                    b.ToTable("brokerTrades");
                });

            modelBuilder.Entity("EasyTrade.DTO.Model.ClientCurrencyTrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BrokerCurrencyTradeId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("BuyAmount")
                        .HasColumnType("numeric");

                    b.Property<long>("BuyCcyId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("SellAmount")
                        .HasColumnType("numeric");

                    b.Property<long>("SellCcyId")
                        .HasColumnType("bigint");

                    b.Property<int>("TradeType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BrokerCurrencyTradeId");

                    b.HasIndex("BuyCcyId");

                    b.HasIndex("SellCcyId");

                    b.ToTable("clientTrades");
                });

            modelBuilder.Entity("EasyTrade.DTO.Model.Currency", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("IsoCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("currencies");
                });

            modelBuilder.Entity("EasyTrade.DTO.Model.Balance", b =>
                {
                    b.HasOne("EasyTrade.DTO.Model.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("EasyTrade.DTO.Model.BrokerCurrencyTrade", b =>
                {
                    b.HasOne("EasyTrade.DTO.Model.Currency", "BuyCcy")
                        .WithMany()
                        .HasForeignKey("BuyCcyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyTrade.DTO.Model.Currency", "SellCcy")
                        .WithMany()
                        .HasForeignKey("SellCcyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuyCcy");

                    b.Navigation("SellCcy");
                });

            modelBuilder.Entity("EasyTrade.DTO.Model.ClientCurrencyTrade", b =>
                {
                    b.HasOne("EasyTrade.DTO.Model.BrokerCurrencyTrade", "BrokerCurrencyTrade")
                        .WithMany()
                        .HasForeignKey("BrokerCurrencyTradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyTrade.DTO.Model.Currency", "BuyCcy")
                        .WithMany()
                        .HasForeignKey("BuyCcyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyTrade.DTO.Model.Currency", "SellCcy")
                        .WithMany()
                        .HasForeignKey("SellCcyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BrokerCurrencyTrade");

                    b.Navigation("BuyCcy");

                    b.Navigation("SellCcy");
                });
#pragma warning restore 612, 618
        }
    }
}
