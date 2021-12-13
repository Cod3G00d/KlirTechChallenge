using System.Linq;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Infrastructure.Database.Context;

namespace KlirTechChallenge.Infrastructure.Domain.Customers;

public class Customers : ICustomers
{
    private readonly KlirTechChallengeContext _dbContext;

    public Customers(KlirTechChallengeContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task Add(Customer customer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Customers.AddAsync(customer, cancellationToken);
    }

    public async Task<Customer> GetById(CustomerId id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Customer> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers
            .Where(c => c.Email == email)
            .FirstOrDefaultAsync(cancellationToken);
    }
}