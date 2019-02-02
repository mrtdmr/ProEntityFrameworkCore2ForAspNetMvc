﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PartyInvites.Models;

namespace PartyInvites.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190131184709_addGuestResponseModelToDb")]
    partial class addGuestResponseModelToDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PartyInvites.Models.GuestResponse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<bool?>("WillAttend");

                    b.HasKey("Id");

                    b.ToTable("GuestResponses");
                });
#pragma warning restore 612, 618
        }
    }
}
