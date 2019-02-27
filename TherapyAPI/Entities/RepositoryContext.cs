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
            modelBuilder.Entity<AppointmentType>().HasData(
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Florais",
                    Code = "FLORAIS"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Acupuntura",
                    Code = "ACUPUNTURA"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Cromoterapia",
                    Code = "CROMOTERAPIA"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Massagem",
                    Code = "MASSAGEM",
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Terapia Com Flores",
                    Code = "TERAPIACOMFLORES",
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Fitoterapia",
                    Code = "FITOTERAPIA"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Reflexologia",
                    Code = "REFLEXOLOGIA"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Shiatsu",
                    Code = "SHIATSU"
                },
                new AppointmentType
                {
                    Id = Guid.NewGuid(),
                    Name = "Reiki",
                    Code = "REIKI"
                }
            );
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentType> AppointmentType { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
    }
}