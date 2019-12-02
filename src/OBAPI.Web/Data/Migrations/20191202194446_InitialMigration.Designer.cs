﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OBAPI.Infra.Data;

namespace OBAPI.Web.Data.Migrations
{
    [DbContext(typeof(OBAPIContext))]
    [Migration("20191202194446_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OBAPI.Domain.Entities.Account", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Category = "Checking",
                            CustomerID = 1,
                            Number = 818181
                        },
                        new
                        {
                            ID = 2,
                            Category = "Checking",
                            CustomerID = 2,
                            Number = 616161
                        });
                });

            modelBuilder.Entity("OBAPI.Domain.Entities.AccountPosting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountID")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal (18, 2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AccountID");

                    b.ToTable("AccountPostings");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            AccountID = 1,
                            Amount = 1521m,
                            Date = new DateTime(2019, 11, 22, 16, 44, 45, 890, DateTimeKind.Local).AddTicks(3999),
                            Description = "Deposito em caixa"
                        },
                        new
                        {
                            ID = 2,
                            AccountID = 1,
                            Amount = -11m,
                            Date = new DateTime(2019, 11, 29, 16, 44, 45, 893, DateTimeKind.Local).AddTicks(6451),
                            Description = "Cobrança de Taxa"
                        });
                });

            modelBuilder.Entity("OBAPI.Domain.Entities.Bank", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Banks");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "OBAPI Bank",
                            Number = 123
                        });
                });

            modelBuilder.Entity("OBAPI.Domain.Entities.Branch", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BankID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BankID");

                    b.ToTable("Branches");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            BankID = 1,
                            Name = "Brach One",
                            Number = 1000
                        },
                        new
                        {
                            ID = 2,
                            BankID = 1,
                            Name = "Brach Two",
                            Number = 2000
                        });
                });

            modelBuilder.Entity("OBAPI.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrachID")
                        .HasColumnType("int");

                    b.Property<int?>("BranchID")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BranchID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            BrachID = 2,
                            Code = "32791181130",
                            Name = "Alice Smith"
                        },
                        new
                        {
                            ID = 2,
                            BrachID = 2,
                            Code = "22691181130",
                            Name = "Bob Smith"
                        });
                });

            modelBuilder.Entity("OBAPI.Domain.Entities.Account", b =>
                {
                    b.HasOne("OBAPI.Domain.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OBAPI.Domain.Entities.AccountPosting", b =>
                {
                    b.HasOne("OBAPI.Domain.Entities.Account", null)
                        .WithMany("AccountPostings")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OBAPI.Domain.Entities.Branch", b =>
                {
                    b.HasOne("OBAPI.Domain.Entities.Bank", "Bank")
                        .WithMany("Branches")
                        .HasForeignKey("BankID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OBAPI.Domain.Entities.Customer", b =>
                {
                    b.HasOne("OBAPI.Domain.Entities.Branch", "Branch")
                        .WithMany("Customers")
                        .HasForeignKey("BranchID");

                    b.HasOne("OBAPI.Domain.Entities.Customer", null)
                        .WithMany("CustomerCards")
                        .HasForeignKey("CustomerID");
                });
#pragma warning restore 612, 618
        }
    }
}