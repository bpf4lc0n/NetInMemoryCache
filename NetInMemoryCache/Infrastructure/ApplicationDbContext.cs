using Bogus;
using Microsoft.EntityFrameworkCore;
using NetInMemoryCache.Models;

namespace NetInMemoryCache.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Client>? Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed database with some random clients
            var id = 1;
            var clients = new Faker<Client>()
                .RuleFor(v => v.Id, f => id++)
                .RuleFor(v => v.FirstName, f => f.Name.FirstName())
                .RuleFor(v => v.LastName, f => f.Name.LastName())
                .RuleFor(v => v.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(v => v.Email, f => f.Person.Email);

            // 400 clients
            modelBuilder
                .Entity<Client>()
                .HasData(clients.GenerateBetween(400, 400));
        }
    }
}
