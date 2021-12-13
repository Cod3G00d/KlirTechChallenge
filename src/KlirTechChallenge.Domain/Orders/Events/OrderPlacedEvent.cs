using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.Core.Events;

namespace KlirTechChallenge.Domain.Orders.Events;

public record class OrderPlacedEvent : DomainEvent
{
    public CustomerId CustomerId { get; init; }
    public OrderId OrderId { get; init; }

    public OrderPlacedEvent(CustomerId customerId, OrderId orderId)
    {
        CustomerId = customerId;
        OrderId = orderId;
        AggregateId = OrderId.Value;
    }
}