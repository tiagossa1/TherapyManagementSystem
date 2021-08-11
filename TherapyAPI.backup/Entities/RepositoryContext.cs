using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TherapyAPI.Models;

namespace TherapyAPI.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData(
                new Gender() {
                    Id = Guid.NewGuid(),
                    Name = "Male"
                },

                new Gender() {
                    Id = Guid.NewGuid(),
                    Name = "Female"
                }
            );
            modelBuilder.Entity<AppointmentType>().HasData(
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Flowers"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Acupuncture"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Chromotherapy"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Massage"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Flower Therapy"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Phytotherapy"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Reflexology"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Shiatsu",
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Reiki"
                }
            );
        }
        
        public DbSet<Client> Clients { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<CivilStatus> CivilStatuses { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentType> AppointmentType { get; set; }
    }
}