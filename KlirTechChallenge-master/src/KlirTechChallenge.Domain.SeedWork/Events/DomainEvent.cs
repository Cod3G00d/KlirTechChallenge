using MediatR;
using System;

namespace KlirTechChallenge.Domain.Core.Events
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
    }

    public abstract  class DomainEvent : Message, IDomainEvent
    {
        public DateTime CreatedAt { get; init; }

        public DomainEvent()
        {
            CreatedAt = DateTime.Now;
        }
    }
}