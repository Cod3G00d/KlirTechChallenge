using System;

namespace KlirTechChallenge.Application.Products
{
    public class ProductViewModel
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Price { get; init; }
        public string CurrencySymbol { get; init; }
    }
}