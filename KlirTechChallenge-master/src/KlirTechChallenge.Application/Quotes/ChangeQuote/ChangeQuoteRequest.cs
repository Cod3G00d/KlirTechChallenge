using System;

namespace KlirTechChallenge.Application.Quotes.ChangeQuote
{
    public class ChangeQuoteRequest
    {
        public Guid QuoteId { get; init; }
        public ProductDto Product { get; init; }
        public string Currency { get; init; }
    }
}