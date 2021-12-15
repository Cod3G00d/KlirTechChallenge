using FluentValidation;
using FluentValidation.Results;
using KlirTechChallenge.Application.Core.CQRS.QueryHandling;
using System;

namespace KlirTechChallenge.Application.Quotes.GetCurrentQuote
{
    public  class GetCurrentQuoteQuery : Query<Guid>
    {
        public Guid CustomerId { get; init; }

        public GetCurrentQuoteQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public override ValidationResult Validate()
        {
            return new GetCurrentQuoteQueryValidator().Validate(this);
        }
    }

    public class GetCurrentQuoteQueryValidator : AbstractValidator<GetCurrentQuoteQuery>
    {
        public GetCurrentQuoteQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotEqual(Guid.Empty).WithMessage("Customer is empty.");
        }
    }
}