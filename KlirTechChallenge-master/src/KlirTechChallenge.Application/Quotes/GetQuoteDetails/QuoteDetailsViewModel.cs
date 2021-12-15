using System.Linq;
using System.Collections.Generic;
using System;

namespace KlirTechChallenge.Application.Quotes.GetQuoteDetails
{
    public class QuoteDetailsViewModel
    {
        public Guid QuoteId { get; set; }
        public List<QuoteItemDetailsViewModel> QuoteItems { get; set; } = new List<QuoteItemDetailsViewModel>();
        public double TotalPrice { get; private set; }
        public string CreatedAt { get; set; }

        public void CalculateTotalOrderPrice()
        {
            TotalPrice = QuoteItems.Sum(s => s.TotalProductPrice);
        }
    }

    public  class QuoteItemDetailsViewModel
    {
        public Guid ProductId { get; init; }
        public String ProductName { get; init; }
        public decimal ProductPrice { get; init; }
        public int ProductQuantity { get; init; }
        public String CurrencySymbol { get; init; }
        public String PromotionName { get; init; }
        public double TotalProductPrice { get; init; }
    }
}