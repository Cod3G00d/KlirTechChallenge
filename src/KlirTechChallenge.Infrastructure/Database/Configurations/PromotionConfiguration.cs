using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.Orders;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Domain.Promotions;
using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KlirTechChallenge.Infrastructure.Database.Configurations;

internal sealed class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.ToTable("Promotions", "dbo");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
        .HasConversion(
            v => v.Value,
            v => new PromotionId(v));

        builder.Property(e => e.CreationDate);
        builder.Property(e => e.Name)
        .HasMaxLength(25).IsRequired();

        builder.Property(e => e.CreationDate);
        builder.Property(e => e.Active)
        .IsRequired();
    }
}