using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using KlirTechChallenge.Application.Core.CQRS.QueryHandling;
using KlirTechChallenge.Application.Core.EventSourcing.StoredEventsData;

namespace KlirTechChallenge.Application.Customers.ListCustomerStoredEvents
{
    public  class ListCustomerStoredEventsQuery : Query<IList<CustomerStoredEventData>>
    {
        public Guid CustomerId { get; init; }

        public ListCustomerStoredEventsQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public override ValidationResult Validate()
        {
            return new ListCustomerStoredEventsQueryValidator().Validate(this);
        }
    }

    public class ListCustomerStoredEventsQueryValidator : AbstractValidator<ListCustomerStoredEventsQuery>
    {
        public ListCustomerStoredEventsQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotEqual(Guid.Empty).WithMessage("CustomerId is empty.");
        }
    }
}