using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TherapyAPI.Models;

namespace TherapyAPI.Entities {
    public class RepositoryContext : DbContext {
        public RepositoryContext (DbContextOptions options) : base (options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
    }
}