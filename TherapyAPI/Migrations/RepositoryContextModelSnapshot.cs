﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TherapyAPI.Entities;

namespace TherapyAPI.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity("TherapyAPI.Models.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AppointmentDate");

                    b.Property<Guid>("AppointmentTypeId");

                    b.Property<Guid>("ClientId");

                    b.Property<Guid>("TherapistId");

                    b.HasKey("Id");

                    b.HasIndex("AppointmentTypeId");

                    b.HasIndex("ClientId");

                    b.HasIndex("TherapistId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("TherapyAPI.Models.AppointmentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("AppointmentType");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d5165995-4328-4ade-a0c8-26b522ce39c0"),
                            Code = "FLORAIS",
                            Name = "Florais"
                        },
                        new
                        {
                            Id = new Guid("513a61b2-884e-44d8-b47b-69e535802dd8"),
                            Code = "ACUPUNTURA",
                            Name = "Acupuntura"
                        },
                        new
                        {
                            Id = new Guid("73f80f13-75b0-48e0-874d-a43e2e444485"),
                            Code = "CROMOTERAPIA",
                            Name = "Cromoterapia"
                        },
                        new
                        {
                            Id = new Guid("a07da813-d2b5-43e3-a995-28fb26836658"),
                            Code = "MASSAGEM",
                            Name = "Massagem"
                        },
                        new
                        {
                            Id = new Guid("bce75384-b900-452f-a60a-0298751a4f2f"),
                            Code = "TERAPIACOMFLORES",
                            Name = "Terapia Com Flores"
                        },
                        new
                        {
                            Id = new Guid("9826cede-c81c-4662-a0d5-cacb1d898b50"),
                            Code = "FITOTERAPIA",
                            Name = "Fitoterapia"
                        },
                        new
                        {
                            Id = new Guid("2296edb8-09ab-4dd7-8b07-2e4b4a9cd118"),
                            Code = "REFLEXOLOGIA",
                            Name = "Reflexologia"
                        },
                        new
                        {
                            Id = new Guid("5859f090-a801-4a16-ab88-a291f8bae425"),
                            Code = "SHIATSU",
                            Name = "Shiatsu"
                        },
                        new
                        {
                            Id = new Guid("1aabf68d-d1c9-41a1-b1f2-bf12b4d7e62b"),
                            Code = "REIKI",
                            Name = "Reiki"
                        });
                });

            modelBuilder.Entity("TherapyAPI.Models.Billing", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AppointmentId");

                    b.Property<Guid>("ClientId");

                    b.Property<decimal>("Price");

                    b.Property<Guid>("TherapistId");

                    b.HasKey("Id");

                    b.ToTable("Billings");
                });

            modelBuilder.Entity("TherapyAPI.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<char>("CivilStatus");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<char>("Gender");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<string>("NIF")
                        .HasMaxLength(9);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Observations");

                    b.Property<string>("Occupation");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("TherapyAPI.Models.Therapist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<char>("Gender");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Therapists");
                });

            modelBuilder.Entity("TherapyAPI.Models.Appointment", b =>
                {
                    b.HasOne("TherapyAPI.Models.AppointmentType", "AppointmentType")
                        .WithMany()
                        .HasForeignKey("AppointmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TherapyAPI.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TherapyAPI.Models.Therapist", "Therapist")
                        .WithMany()
                        .HasForeignKey("TherapistId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
