using System.Linq;
using KlirTechChallenge.Domain;
using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.Orders;
using KlirTechChallenge.Domain.Payments;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Domain.Core.Events;
using KlirTechChallenge.Infrastructure.Events;
using KlirTechChallenge.Infrastructure.Database.Context;
using KlirTechChallenge.Domain.Promotions;

namespace KlirTechChallenge.Infrastructure.Domain;

public class EcommerceUnitOfWork : UnitOfWork<KlirTechChallengeContext>, IEcommerceUnitOfWork
{
    public ICustomers Customers { get; }
    public IOrders Orders { get; }
    public IStoredEvents StoredEvents { get; }
    public IProducts Products { get; }
    public IQuotes Quotes { get; }
    public IPayments Payments { get; }
    public IPromotions Promotions { get; }


    private readonly IEventSerializer _eventSerializer;

    public EcommerceUnitOfWork(KlirTechChallengeContext dbContext,
        ICustomers customers,
        IOrders orders,
        IStoredEvents storedEvents,
        IProducts products,
        IPayments payments,
        IQuotes quotes,
        IPromotions promotions,
        IEventSerializer eventSerializer) : base(dbContext)
    {
        Customers = customers ?? throw new ArgumentNullException(nameof(customers));
        Orders = orders ?? throw new ArgumentNullException(nameof(orders));
        StoredEvents = storedEvents ?? throw new ArgumentNullException(nameof(storedEvents));
        Products = products ?? throw new ArgumentNullException(nameof(products));
        Quotes = quotes ?? throw new ArgumentNullException(nameof(quotes));
        Payments = payments ?? throw new ArgumentNullException(nameof(payments));
        Promotions = promotions ?? throw new ArgumentNullException(nameof(payments));

        _eventSerializer = eventSerializer ?? throw new ArgumentNullException(nameof(eventSerializer));
    }

    protected async override Task StoreEvents(CancellationToken cancellationToken)
    {
        var entities = DbContext.ChangeTracker.Entries()
            .Where(e => e.Entity is IAggregateRoot c && c.DomainEvents != null)
            .Select(e => e.Entity as IAggregateRoot)
            .ToArray();

        foreach (var entity in entities)
        {
            var messages = entity.DomainEvents
                .Select(e => StoredEventHelper.BuildFromDomainEvent(e as DomainEvent, _eventSerializer))
                .ToArray();

            entity.ClearDomainEvents();
            await DbContext.AddRangeAsync(messages, cancellationToken);
        }
    }
}
