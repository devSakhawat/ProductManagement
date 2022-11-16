﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductManagement.DAL;

#nullable disable

namespace ProductManagement.DAL.Migrations
{
    [DbContext(typeof(TonerContext))]
    partial class TonerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ProductManagement.Domain.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.DeliveryToner", b =>
                {
                    b.Property<int>("DeliveryTonerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeliveryTonerId"), 1L, 1);

                    b.Property<double?>("BW")
                        .HasColumnType("float");

                    b.Property<double?>("Black")
                        .HasColumnType("float");

                    b.Property<double?>("ColourTotal")
                        .HasColumnType("float");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<double?>("Cyan")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<double?>("Magenta")
                        .HasColumnType("float");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<double?>("Yellow")
                        .HasColumnType("float");

                    b.HasKey("DeliveryTonerId");

                    b.HasIndex("MachineId");

                    b.ToTable("DeliveryToners");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.Machine", b =>
                {
                    b.Property<int>("MachineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MachineId"), 1L, 1);

                    b.Property<int>("ColourType")
                        .HasColumnType("int");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MachineModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MachineSN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.HasKey("MachineId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.PaperUsage", b =>
                {
                    b.Property<int>("PaperUsageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaperUsageId"), 1L, 1);

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<long>("CurrentCounter")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<long>("MonthlyTotalCounter")
                        .HasColumnType("bigint");

                    b.Property<long>("PreviousCounter")
                        .HasColumnType("bigint");

                    b.HasKey("PaperUsageId");

                    b.HasIndex("MachineId");

                    b.ToTable("PaperUsages");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.ProfitCalculation", b =>
                {
                    b.Property<int>("CalculationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalculationId"), 1L, 1);

                    b.Property<double>("CounterPerToner")
                        .HasColumnType("float");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFrofitable")
                        .HasColumnType("bit");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.HasKey("CalculationId");

                    b.HasIndex("MachineId");

                    b.ToTable("ProfitCalculations");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"), 1L, 1);

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ProjectId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.Toner", b =>
                {
                    b.Property<int>("TonerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TonerId"), 1L, 1);

                    b.Property<int>("Color")
                        .HasColumnType("int");

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("smalldatetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("MachineId")
                        .HasColumnType("int");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<string>("SerialNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TonerModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TonerId");

                    b.HasIndex("MachineId");

                    b.ToTable("Toners");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.TonerUsage", b =>
                {
                    b.Property<int>("TonerUsageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TonerUsageId"), 1L, 1);

                    b.Property<long?>("CreatedBy")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("smalldatetime");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("DeliveryTonerId")
                        .HasColumnType("int");

                    b.Property<int>("InHouse")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<long?>("ModifiedBy")
                        .HasColumnType("bigint");

                    b.Property<double>("MonthlyTotalToner")
                        .HasColumnType("float");

                    b.Property<double>("MonthlyUsedToner")
                        .HasColumnType("float");

                    b.Property<double?>("PercentageBW")
                        .HasColumnType("float");

                    b.Property<double?>("PercentageBlack")
                        .HasColumnType("float");

                    b.Property<double?>("PercentageCyan")
                        .HasColumnType("float");

                    b.Property<double?>("PercentageMagenta")
                        .HasColumnType("float");

                    b.Property<double?>("PercentageYellow")
                        .HasColumnType("float");

                    b.Property<double?>("TotalColurParcentage")
                        .HasColumnType("float");

                    b.Property<double>("TotalToner")
                        .HasColumnType("float");

                    b.HasKey("TonerUsageId");

                    b.HasIndex("DeliveryTonerId");

                    b.HasIndex("MachineId");

                    b.ToTable("TonerUsages");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.DeliveryToner", b =>
                {
                    b.HasOne("ProductManagement.Domain.Entities.Machine", "Machine")
                        .WithMany("DeliveryToners")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.Machine", b =>
                {
                    b.HasOne("ProductManagement.Domain.Entities.Project", "Project")
                        .WithMany("Machines")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.PaperUsage", b =>
                {
                    b.HasOne("ProductManagement.Domain.Entities.Machine", "Machine")
                        .WithMany("PaperUsages")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.ProfitCalculation", b =>
                {
                    b.HasOne("ProductManagement.Domain.Entities.Machine", "Machine")
                        .WithMany("ProfitCalculations")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.Project", b =>
                {
                    b.HasOne("ProductManagement.Domain.Entities.Customer", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.Toner", b =>
                {
                    b.HasOne("ProductManagement.Domain.Entities.Machine", "Machines")
                        .WithMany("Toner")
                        .HasForeignKey("MachineId");

                    b.Navigation("Machines");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.TonerUsage", b =>
                {
                    b.HasOne("ProductManagement.Domain.Entities.DeliveryToner", null)
                        .WithMany("TonerUsages")
                        .HasForeignKey("DeliveryTonerId");

                    b.HasOne("ProductManagement.Domain.Entities.Machine", "Machine")
                        .WithMany("TonerUsages")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.DeliveryToner", b =>
                {
                    b.Navigation("TonerUsages");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.Machine", b =>
                {
                    b.Navigation("DeliveryToners");

                    b.Navigation("PaperUsages");

                    b.Navigation("ProfitCalculations");

                    b.Navigation("Toner");

                    b.Navigation("TonerUsages");
                });

            modelBuilder.Entity("ProductManagement.Domain.Entities.Project", b =>
                {
                    b.Navigation("Machines");
                });
#pragma warning restore 612, 618
        }
    }
}
