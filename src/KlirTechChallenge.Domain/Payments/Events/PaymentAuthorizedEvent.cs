using KlirTechChallenge.Domain.Core.Events;

namespace KlirTechChallenge.Domain.Payments.Events;

public record class PaymentAuthorizedEvent : DomainEvent
{
    public PaymentId PaymentId { get; init; }

    public PaymentAuthorizedEvent(PaymentId paymentId)
    {
        PaymentId = paymentId;
        AggregateId = paymentId.Value;
    }
}
