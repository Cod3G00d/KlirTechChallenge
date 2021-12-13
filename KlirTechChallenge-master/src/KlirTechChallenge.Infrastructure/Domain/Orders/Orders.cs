using System.Linq;
using System.Collections.Generic;
using KlirTechChallenge.Domain.Orders;
using KlirTechChallenge.Infrastructure.Database.Context;

namespace KlirTechChallenge.Infrastructure.Domain.Orders;

public class Orders : IOrders
{
    private readonly KlirTechChallengeContext _dbContext;

    public Orders(KlirTechChallengeContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task Add(Order order, CancellationToken cancellationToken = default)
    {
        await _dbContext.Orders.AddAsync(order, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> Find(Specification<Order> specification, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .Where(specification.ToExpression())
            .ToListAsync();
    }

    public async Task<Order> GetById(OrderId orderId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .Where(c => c.Id == orderId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}