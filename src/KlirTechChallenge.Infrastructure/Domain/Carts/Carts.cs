using System.Linq;
using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Infrastructure.Database.Context;

namespace KlirTechChallenge.Infrastructure.Domain.Quotes;

public class Quotes : IQuotes
{
    private readonly KlirTechChallengeContext _context;

    public Quotes(KlirTechChallengeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(Quote quote, CancellationToken cancellationToken = default)
    {
        await _context.Quotes.AddAsync(quote, cancellationToken);
    }

    public async Task<Quote> GetById(QuoteId quoteId, CancellationToken cancellationToken = default)
    {
        return await _context.Quotes.FirstOrDefaultAsync(x => x.Id == quoteId, cancellationToken);
    }

    public async Task<Quote> GetCurrentQuote(CustomerId customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Quotes.OrderByDescending(t => t.CreationDate)
            .FirstOrDefaultAsync(x => x.CustomerId == customerId, cancellationToken);
    }
}