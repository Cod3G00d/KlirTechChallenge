using System;
using System.Linq.Expressions;
using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.SeedWork;

namespace KlirTechChallenge.Domain.Orders.Specifications
{
    public class IsOrderFromQuote : Specification<Order>
    {
        private readonly QuoteId _quoteId;

        public IsOrderFromQuote(QuoteId quoteId)
        {
            _quoteId = quoteId;
        }

        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.QuoteId == _quoteId;
        }
    }
}