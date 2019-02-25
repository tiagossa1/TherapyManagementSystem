using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TherapyAPI.Models;

namespace TherapyAPI.Entities {
    public class RepositoryContext : DbContext {
        public RepositoryContext (DbContextOptions options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<AppointmentType> ().HasData (
                new AppointmentType {
                    Name = "Florais",
                        Code = "FLORAIS"
                },
                new AppointmentType {
                    Name = "Acupuntura",
                        Code = "ACUPUNTURA"
                },
                new AppointmentType {
                    Name = "Cromoterapia",
                        Code = "CROMOTERAPIA"
                },
                new AppointmentType {
                    Name = "Massagem",
                        Code = "MASSAGEM",
                },
                new AppointmentType {
                    Name = "Terapia Com Flores",
                        Code = "TERAPIACOMFLORES",
                },
                new AppointmentType {
                    Name = "Fitoterapia",
                        Code = "FITOTERAPIA"
                },
                new AppointmentType {
                    Name = "Reflexologia",
                        Code = "REFLEXOLOGIA"
                },
                new AppointmentType {
                    Name = "Shiatsu",
                        Code = "SHIATSU"
                },
                new AppointmentType {
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