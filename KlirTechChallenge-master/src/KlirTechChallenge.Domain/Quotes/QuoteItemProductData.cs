using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Domain.SharedKernel;

namespace KlirTechChallenge.Domain.Quotes
{
    public  class QuoteItemProductData
    {
        public ProductId ProductId { get; set; }
        public Money ProductPrice { get; set; }
        public int Quantity { get; set; }
        public string PromotionName { get; set; }
        public decimal TotalPrice { get; set; }


        public QuoteItemProductData(ProductId productId, Money productPrice, int quantity, string promotionName, decimal totalPrice)
        {
            ProductId = productId;
            ProductPrice = productPrice;
            Quantity = quantity;
            PromotionName = promotionName;
            TotalPrice = totalPrice;

        }
    }
}