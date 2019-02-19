using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuddhaTerapiasAPI.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace BuddhaTerapiasAPI.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Dev> Developers { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=buddha.db");
        }
    }
}
