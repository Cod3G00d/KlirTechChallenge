using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.SeedWork;

namespace KlirTechChallenge.Domain.Quotes;

public interface IQuotes : IRepository<Quote>
{
    Task Add(Quote quote, CancellationToken cancellationToken = default);
    Task<Quote> GetById(QuoteId quoteId, CancellationToken cancellationToken = default);
    Task<Quote> GetCurrentQuote(CustomerId customerId, CancellationToken cancellationToken = default);
}