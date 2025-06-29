﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using tmsminimalapi.Data;

#nullable disable

namespace tmsminimalapi.Migrations
{
    [DbContext(typeof(TmsDbContext))]
    [Migration("20250611024010_AddPartyTable")]
    partial class AddPartyTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("tmsminimalapi.Models.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("booking_date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("DestinationCity")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("destination_city");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext")
                        .HasColumnName("notes");

                    b.Property<decimal>("PaidByParty")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("paid_by_party");

                    b.Property<Guid>("PartyId")
                        .HasColumnType("char(36)")
                        .HasColumnName("party_id");

                    b.Property<string>("SourceCity")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("source_city");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("total_amount");

                    b.HasKey("Id");

                    b.HasIndex("PartyId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("tmsminimalapi.Models.Party", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("created_at");

                    b.Property<string>("PartyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("party_name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("updated_at");

                    b.HasKey("Id");

                    b.HasIndex("PartyName");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("tmsminimalapi.Models.Booking", b =>
                {
                    b.HasOne("tmsminimalapi.Models.Party", "Party")
                        .WithMany("Bookings")
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Party");
                });

            modelBuilder.Entity("tmsminimalapi.Models.Party", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
