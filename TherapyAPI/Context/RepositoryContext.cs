using Microsoft.EntityFrameworkCore;
using System;
using TherapyAPI.Models;

namespace TherapyAPI.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            PopulateDatabase(builder);

            builder.Entity<Appointment>().HasKey(a => a.Id);
            builder.Entity<AppointmentType>().HasKey(at => at.Id);
            builder.Entity<Billing>().HasKey(b => b.Id);
            builder.Entity<CivilStatus>().HasKey(cs => cs.Id);
            builder.Entity<Client>().HasKey(c => c.Id);
            builder.Entity<Gender>().HasKey(g => g.Id);
            builder.Entity<Therapist>().HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        private static void PopulateDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData(
                new Gender()
                {
                    Id = Guid.NewGuid(),
                    Name = "Male"
                },

                new Gender()
                {
                    Id = Guid.NewGuid(),
                    Name = "Female"
                }
            );

            modelBuilder.Entity<CivilStatus>().HasData(
                new CivilStatus()
                {
                    Id = Guid.NewGuid(),
                    Name = "Married"
                },
                new CivilStatus()
                {
                    Id = Guid.NewGuid(),
                    Name = "Divorced"
                },
                new CivilStatus()
                {
                    Id = Guid.NewGuid(),
                    Name = "Single"
                });

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