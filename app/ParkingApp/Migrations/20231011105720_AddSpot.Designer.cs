﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingApp.Model.EntityFramework;

#nullable disable

namespace ParkingApp.Migrations
{
    [DbContext(typeof(ParkingContext))]
    [Migration("20231011105720_AddSpot")]
    partial class AddSpot
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("ParkingApp.Model.EntityFramework.Booking", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CarPlateNumber")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "car_plate_number");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "date");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasAnnotation("Relational:JsonPropertyName", "status");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Bookings");

                    b.HasAnnotation("Relational:JsonPropertyName", "bookings");
                });

            modelBuilder.Entity("ParkingApp.Model.EntityFramework.Employee", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Balance")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "balance");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("MonthlyLimit")
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Relational:JsonPropertyName", "monthly_limit");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ParkingApp.Model.EntityFramework.Manager", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SecretCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("ParkingApp.Model.EntityFramework.Spot", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BookingId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpotCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.ToTable("Spots");
                });

            modelBuilder.Entity("ParkingApp.Model.EntityFramework.Booking", b =>
                {
                    b.HasOne("ParkingApp.Model.EntityFramework.Employee", "Employee")
                        .WithMany("Bookings")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("ParkingApp.Model.EntityFramework.Spot", b =>
                {
                    b.HasOne("ParkingApp.Model.EntityFramework.Booking", "Booking")
                        .WithMany()
                        .HasForeignKey("BookingId");

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("ParkingApp.Model.EntityFramework.Employee", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
