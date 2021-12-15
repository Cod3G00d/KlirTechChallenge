using KlirTechChallenge.Domain.SeedWork;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Domain.SharedKernel;
using System;

namespace KlirTechChallenge.Domain.Quotes
{
    public class QuoteItem : Entity<Guid>
    {
        public ProductId ProductId { get; private set; }
        public int Quantity { get; private set; }
        public double TotalPrice { get; set; }
        public QuoteItem(Guid id, ProductId productId, int quantity, double totalPrice)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            TotalPrice = totalPrice;
        }

        public void ChangeQuantity(int quantity)
        {
            if (quantity == 0)
                throw new BusinessRuleException("The product quantity must be at last 1.");

            Quantity = quantity;
        }

        public void ChangeTotalPrice(double totalPrice)
        {
            TotalPrice = totalPrice;
        }


        // Empty constructor for EF
        private QuoteItem() { }
    }
}