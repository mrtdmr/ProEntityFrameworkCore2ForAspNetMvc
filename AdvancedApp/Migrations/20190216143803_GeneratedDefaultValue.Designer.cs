﻿// <auto-generated />
using AdvancedApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AdvancedApp.Migrations
{
    [DbContext(typeof(AdvancedContext))]
    [Migration("20190216143803_GeneratedDefaultValue")]
    partial class GeneratedDefaultValue
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdvancedApp.Models.Employee", b =>
                {
                    b.Property<string>("SSN");

                    b.Property<string>("FirstName");

                    b.Property<string>("FamilyName");

                    b.Property<string>("GeneratedValue")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(new DateTime(2019, 2, 16, 17, 38, 3, 190, DateTimeKind.Local));

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(8,2)")
                        .HasAnnotation("PropertyAccessMode", PropertyAccessMode.Field);

                    b.Property<bool>("SoftDeleted");

                    b.HasKey("SSN", "FirstName", "FamilyName");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("AdvancedApp.Models.SecondaryIdentity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("InActiveUse");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("PrimaryFamilyName");

                    b.Property<string>("PrimaryFirstName");

                    b.Property<string>("PrimarySSN");

                    b.HasKey("Id");

                    b.HasIndex("PrimarySSN", "PrimaryFirstName", "PrimaryFamilyName")
                        .IsUnique()
                        .HasFilter("[PrimarySSN] IS NOT NULL AND [PrimaryFirstName] IS NOT NULL AND [PrimaryFamilyName] IS NOT NULL");

                    b.ToTable("SecondaryIdentity");
                });

            modelBuilder.Entity("AdvancedApp.Models.SecondaryIdentity", b =>
                {
                    b.HasOne("AdvancedApp.Models.Employee", "PrimaryIdentity")
                        .WithOne("OtherIdentity")
                        .HasForeignKey("AdvancedApp.Models.SecondaryIdentity", "PrimarySSN", "PrimaryFirstName", "PrimaryFamilyName")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
