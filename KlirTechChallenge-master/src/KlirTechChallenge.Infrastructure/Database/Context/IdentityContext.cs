using KlirTechChallenge.Infrastructure.Identity.Users;
using KlirTechChallenge.Infrastructure.Identity.Roles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace KlirTechChallenge.Infrastructure.Database.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, UserRole, Guid>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.Property(u => u.Id)
                    .HasDefaultValueSql("newsequentialid()");
            });

            modelBuilder.Entity<UserRole>(b =>
            {
                b.Property(u => u.Id)
                    .HasDefaultValueSql("newsequentialid()");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}