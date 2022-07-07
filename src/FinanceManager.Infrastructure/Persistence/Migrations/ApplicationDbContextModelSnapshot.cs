﻿// <auto-generated />
using System;
using FinanceManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceManager.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FinanceManager.Domain.TransactionsAggregate.Transaction", b =>
                {
                    b.Property<Guid?>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("AmountInEur")
                        .HasColumnType("numeric");

                    b.Property<DateOnly>("BookingDate")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("IntendedUse")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsRecurring")
                        .HasColumnType("boolean");

                    b.Property<string>("Recipient")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Reimbursable")
                        .HasColumnType("boolean");

                    b.HasKey("TransactionId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("FinanceManager.Domain.TransactionsAggregate.Transaction", b =>
                {
                    b.OwnsOne("FinanceManager.Domain.AccountsAggregate.BankAccount", "BankAccount", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uuid");

                            b1.Property<string>("BankName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Isin")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("TransactionId");

                            b1.ToTable("Expenses");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");
                        });

                    b.OwnsOne("FinanceManager.Domain.TransactionsAggregate.Category", "Category", b1 =>
                        {
                            b1.Property<Guid>("TransactionId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("TransactionId");

                            b1.ToTable("Expenses");

                            b1.WithOwner()
                                .HasForeignKey("TransactionId");
                        });

                    b.Navigation("BankAccount")
                        .IsRequired();

                    b.Navigation("Category")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
