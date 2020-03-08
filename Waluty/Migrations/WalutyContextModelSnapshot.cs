﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Waluty.Data;

namespace Waluty.Migrations
{
    [DbContext(typeof(WalutyContext))]
    partial class WalutyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Waluty.Models.ExchangeRate", b =>
                {
                    b.Property<int>("ExchangeRateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Table")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExchangeRateId");

                    b.ToTable("ExchangeRate");
                });

            modelBuilder.Entity("Waluty.Models.Rate", b =>
                {
                    b.Property<int>("RateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EffectiveDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExchangeRateId")
                        .HasColumnType("int");

                    b.Property<decimal>("MidPrice")
                        .HasColumnType("decimal(18, 4)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RateId");

                    b.HasIndex("ExchangeRateId");

                    b.ToTable("Rate");
                });

            modelBuilder.Entity("Waluty.Models.Rate", b =>
                {
                    b.HasOne("Waluty.Models.ExchangeRate", null)
                        .WithMany("Rates")
                        .HasForeignKey("ExchangeRateId");
                });
#pragma warning restore 612, 618
        }
    }
}
