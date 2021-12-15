using KlirTechChallenge.Domain.Promotions;
using KlirTechChallenge.Domain.SeedWork;
using KlirTechChallenge.Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace KlirTechChallenge.Domain.Products
{
    public class Product : AggregateRoot<ProductId>
    {
        public PromotionId PromotionId { get; private set; }
        public string Name { get; private set; }
        public Money Price { get; private set; }
        public DateTime CreationDate { get; }


        public static Product CreateNew(string name, Money price, PromotionId promotionId)
        {
            return new Product(ProductId.Of(Guid.NewGuid()), name, price, promotionId);
        }

        private Product(ProductId id, string name, Money price, PromotionId promotionId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be null or whitespace.", nameof(name));

            Id = id;
            Name = name;
            Price = price ?? throw new ArgumentNullException(nameof(price));
            PromotionId = promotionId;
            CreationDate = DateTime.Now;
        }

        // Empty constructor for EF
        private Product() { }
    }
}