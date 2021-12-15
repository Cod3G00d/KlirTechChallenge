using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KlirTechChallenge.Infrastructure.Database.Configurations
{
    internal sealed class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.ToTable("Quotes", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new QuoteId(v));

            builder.Property(e => e.CustomerId)
            .HasConversion(guid => guid.Value, s => new CustomerId(s));

            builder.Property(e => e.CreationDate);

            builder.OwnsMany(s => s.Items, b =>
            {
                b.WithOwner().HasForeignKey("QuoteId");

                b.ToTable("QuoteItems", "dbo")
                .HasKey(x => x.Id);

                b.Property(e => e.Id).IsRequired()
                .ValueGeneratedNever();

                b.Property(e => e.ProductId)
                .HasConversion(guid => guid.Value, s => new ProductId(s));

                b.Property(e => e.Quantity);

                b.Property(e => e.TotalPrice);
            });
        }
    }
}