using System.Collections.Generic;
using KlirTechChallenge.Domain.Core.Events;

namespace KlirTechChallenge.Domain.SeedWork
{
    /// <summary>
    ///  Aggregate root interface
    /// </summary>
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
        void ClearDomainEvents();
    }
}