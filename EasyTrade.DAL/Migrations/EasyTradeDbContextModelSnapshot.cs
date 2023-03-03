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

            modelBuilder.Entity("EasyTrade.Domain.Model.Balance", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("CurrencyIso")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.Property<Guid>("Version")
                        .IsConcurrencyToken()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyIso");

                    b.ToTable("Balances");
                });

            modelBuilder.Entity("EasyTrade.Domain.Model.BrokerCurrencyTrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("BuyAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("BuyCurrencyIso")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("SellAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("SellCurrencyIso")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.Property<int>("TradeType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BuyCurrencyIso");

                    b.HasIndex("SellCurrencyIso");

                    b.ToTable("BrokerTrades");
                });

            modelBuilder.Entity("EasyTrade.Domain.Model.ClientCurrencyTrade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("BrokerCurrencyTradeId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("BuyAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("BuyCurrencyIso")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("SellAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("SellCurrencyIso")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.Property<int>("TradeType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BrokerCurrencyTradeId");

                    b.HasIndex("BuyCurrencyIso");

                    b.HasIndex("SellCurrencyIso");

                    b.ToTable("ClientTrades");
                });

            modelBuilder.Entity("EasyTrade.Domain.Model.Currency", b =>
                {
                    b.Property<string>("IsoCode")
                        .HasColumnType("char(3)");

                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IsoCode");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("EasyTrade.Domain.Model.CurrencyTradeCoefficient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Coefficient")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstCcyIsoCode")
                        .HasColumnType("char(3)");

                    b.Property<string>("SecondCcyIsoCode")
                        .HasColumnType("char(3)");

                    b.HasKey("Id");

                    b.HasIndex("FirstCcyIsoCode");

                    b.HasIndex("SecondCcyIsoCode");

                    b.ToTable("Coefficients");
                });

            modelBuilder.Entity("EasyTrade.Domain.Model.Operation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("CurrencyIso")
                        .IsRequired()
                        .HasColumnType("char(3)");

                    b.Property<DateTimeOffset>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyIso");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("EasyTrade.Domain.Model.Balance", b =>
                {
                    b.HasOne("EasyTrade.Domain.Model.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyIso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("EasyTrade.Domain.Model.BrokerCurrencyTrade", b =>
                {
                    b.HasOne("EasyTrade.Domain.Model.Currency", "BuyCcy")
                        .WithMany()
                        .HasForeignKey("BuyCurrencyIso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyTrade.Domain.Model.Currency", "SellCcy")
                        .WithMany()
                        .HasForeignKey("SellCurrencyIso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BuyCcy");

                    b.Navigation("SellCcy");
                });

            modelBuilder.Entity("EasyTrade.Domain.Model.ClientCurrencyTrade", b =>
                {
                    b.HasOne("EasyTrade.Domain.Model.BrokerCurrencyTrade", "BrokerCurrencyTrade")
                        .WithMany()
                        .HasForeignKey("BrokerCurrencyTradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyTrade.Domain.Model.Currency", "BuyCcy")
                        .WithMany()
                        .HasForeignKey("BuyCurrencyIso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EasyTrade.Domain.Model.Currency", "SellCcy")
                        .WithMany()
                        .HasForeignKey("SellCurrencyIso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BrokerCurrencyTrade");

                    b.Navigation("BuyCcy");

                    b.Navigation("SellCcy");
                });

            modelBuilder.Entity("EasyTrade.Domain.Model.CurrencyTradeCoefficient", b =>
                {
                    b.HasOne("EasyTrade.Domain.Model.Currency", "FirstCcy")
                        .WithMany()
                        .HasForeignKey("FirstCcyIsoCode");

                    b.HasOne("EasyTrade.Domain.Model.Currency", "SecondCcy")
                        .WithMany()
                        .HasForeignKey("SecondCcyIsoCode");

                    b.Navigation("FirstCcy");

                    b.Navigation("SecondCcy");
                });

            modelBuilder.Entity("EasyTrade.Domain.Model.Operation", b =>
                {
                    b.HasOne("EasyTrade.Domain.Model.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyIso")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");
                });
#pragma warning restore 612, 618
        }
    }
}
