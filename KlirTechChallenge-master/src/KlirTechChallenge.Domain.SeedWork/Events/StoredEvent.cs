using System;

namespace KlirTechChallenge.Domain.Core.Events
{
    public  class StoredEvent : DomainEvent
    {
        public Guid Id { get; init; }
        public string Payload { get; init; }
        public DateTime? ProcessedAt { get; private set; }

        public StoredEvent(DomainEvent @event, string payload)
        {
            Id = Guid.NewGuid();
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            Payload = payload;
        }

        public void SetProcessedAt(DateTime? date)
        {
            if (date == null)
                throw new ArgumentNullException(nameof(date));

            ProcessedAt = date;
        }

        // EF Constructor
        protected StoredEvent() { }
    }
}