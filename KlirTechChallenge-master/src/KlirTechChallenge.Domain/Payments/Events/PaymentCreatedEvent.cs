using KlirTechChallenge.Domain.Core.Events;

namespace KlirTechChallenge.Domain.Payments.Events
{
    public  class PaymentCreatedEvent : DomainEvent
    {
        public PaymentId PaymentId { get; init; }

        public PaymentCreatedEvent(PaymentId paymentId)
        {
            PaymentId = paymentId;
            AggregateId = paymentId.Value;
        }
    }
}