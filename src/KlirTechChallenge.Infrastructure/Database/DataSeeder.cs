using System.Linq;
using System.Collections.Generic;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Domain.SharedKernel;
using KlirTechChallenge.Infrastructure.Database.Context;
using KlirTechChallenge.Domain.Promotions;

namespace KlirTechChallenge.Infrastructure.Database;

public static class DataSeeder
{
    public static void SeedData(KlirTechChallengeContext context)
    {

        if (!context.Promotions.Any())
        {
            // Creating products
            var promotions = new List<Promotion>();
            var promotionId = PromotionId.Of(Guid.NewGuid());
            var promotion = Promotion.CreateNew("Buy 1 Get 1 Free", true);
            promotions.Add(promotion);
            promotion = Promotion.CreateNew("3 for 10 Euro", true);
            promotions.Add(promotion);
            context.AddRange(promotions);
            context.SaveChanges();
        }

        if (!context.Products.Any())
        {
            // Creating products
            var products = new List<Product>();
            var rand = new Random();


            var price = new decimal(20);
            var promotionId = context.Promotions.FirstOrDefault(x => x.Name == "Buy 1 Get 1 Free");
            var productId = ProductId.Of(Guid.NewGuid());
            var product = Product.CreateNew($"Product {'A'}", Money.Of(price, Currency.USDollar.Code), promotionId.Id);
            
            products.Add(product);


             price = new decimal(4);
             promotionId = context.Promotions.FirstOrDefault(x => x.Name == "3 for 10 Euro");
             productId = ProductId.Of(Guid.NewGuid());
             product = Product.CreateNew($"Product {'B'}", Money.Of(price, Currency.USDollar.Code), promotionId.Id);

             products.Add(product);



             price = new decimal(2);
             productId = ProductId.Of(Guid.NewGuid());
             product = Product.CreateNew($"Product {'C'}", Money.Of(price, Currency.USDollar.Code), null);

             products.Add(product);


             price = new decimal(4);
             promotionId = context.Promotions.FirstOrDefault(x => x.Name == "3 for 10 Euro");
             productId = ProductId.Of(Guid.NewGuid());
             product = Product.CreateNew($"Product {'D'}", Money.Of(price, Currency.USDollar.Code), null);

            products.Add(product);


            context.AddRange(products);
            context.SaveChanges();
        }
    }
}