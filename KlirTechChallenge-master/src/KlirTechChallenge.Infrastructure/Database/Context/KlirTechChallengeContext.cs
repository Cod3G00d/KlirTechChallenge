using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.Orders;
using KlirTechChallenge.Domain.Payments;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using KlirTechChallenge.Domain.Core.Events;
using KlirTechChallenge.Domain.Promotions;

namespace KlirTechChallenge.Infrastructure.Database.Context
{
    public sealed class KlirTechChallengeContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

        public KlirTechChallengeContext(DbContextOptions<KlirTechChallengeContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<DomainEvent>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(KlirTechChallengeContext).Assembly);
        }
    }
}