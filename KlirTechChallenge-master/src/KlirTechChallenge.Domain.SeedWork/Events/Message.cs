using System;

namespace KlirTechChallenge.Domain.Core.Events
{
    public abstract  class Message
    {
        public string MessageType { get; init; }
        public Guid AggregateId { get; init; }

        protected Message()
        {
            MessageType = GetType().FullName;
        }
    }
}