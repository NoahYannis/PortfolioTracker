﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortfolioTrackerServer.Data;

#nullable disable

namespace PortfolioTrackerServer.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PortfolioTrackerShared.Models.Order", b =>
                {
                    b.Property<int>("OrderNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderNumber"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderType")
                        .HasColumnType("int");

                    b.Property<int?>("PortfolioId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderNumber");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PortfolioTrackerShared.Models.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PortfolioValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("PortfolioTrackerShared.Models.PortfolioStock", b =>
                {
                    b.Property<string>("Ticker")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<decimal?>("AbsolutePerformance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("BuyInPrice")
                        .IsRequired()
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal?>("CurrentPrice")
                        .HasColumnType("decimal(8,2)");

                    b.Property<decimal?>("DividendYield")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Industry")
                        .HasColumnType("int");

                    b.Property<int?>("PortfolioId")
                        .HasColumnType("int");

                    b.Property<decimal?>("PositionSize")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("RelativePerformance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SharesOwned")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Ticker");

                    b.HasIndex("PortfolioId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("PortfolioTrackerShared.Models.UserModels.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PortfolioTrackerShared.Models.UserModels.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ColorScheme")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvestingGoals")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("PortfolioTrackerShared.Models.Order", b =>
                {
                    b.HasOne("PortfolioTrackerShared.Models.Portfolio", null)
                        .WithMany("OrderHistory")
                        .HasForeignKey("PortfolioId");
                });

            modelBuilder.Entity("PortfolioTrackerShared.Models.PortfolioStock", b =>
                {
                    b.HasOne("PortfolioTrackerShared.Models.Portfolio", null)
                        .WithMany("Positions")
                        .HasForeignKey("PortfolioId");
                });

            modelBuilder.Entity("PortfolioTrackerShared.Models.UserModels.UserSettings", b =>
                {
                    b.HasOne("PortfolioTrackerShared.Models.UserModels.User", null)
                        .WithOne("Settings")
                        .HasForeignKey("PortfolioTrackerShared.Models.UserModels.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PortfolioTrackerShared.Models.Portfolio", b =>
                {
                    b.Navigation("OrderHistory");

                    b.Navigation("Positions");
                });

            modelBuilder.Entity("PortfolioTrackerShared.Models.UserModels.User", b =>
                {
                    b.Navigation("Settings")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
