using KlirTechChallenge.Domain.Core.Events;

namespace KlirTechChallenge.Domain.Customers.Events
{
    public class CustomerUpdatedEvent : DomainEvent
    {
        public CustomerId CustomerId { get; init; }
        public string Name { get; init; }

        public CustomerUpdatedEvent(CustomerId customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
            AggregateId = CustomerId.Value;
        }
    }
}