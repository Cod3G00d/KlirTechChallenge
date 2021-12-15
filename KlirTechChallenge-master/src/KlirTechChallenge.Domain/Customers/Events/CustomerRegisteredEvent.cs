using KlirTechChallenge.Domain.Core.Events;

namespace KlirTechChallenge.Domain.Customers.Events
{
    public  class CustomerRegisteredEvent : DomainEvent
    {
        public CustomerId CustomerId { get; init; }
        public string Name { get; init; }

        public CustomerRegisteredEvent(CustomerId customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
            AggregateId = customerId.Value;
        }
    }
}