using System;

namespace KlirTechChallenge.Application.Quotes.SaveQuote
{
    public class  CreateQuoteRequest
    {
        public Guid CustomerId { get; init; }
        public ProductDto Product { get; init; }
        public string Currency { get; init; }
    }
}