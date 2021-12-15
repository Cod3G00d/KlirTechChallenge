using FluentValidation;
using FluentValidation.Results;
using KlirTechChallenge.Application.Core.CQRS.CommandHandling;
using System;

namespace KlirTechChallenge.Application.Quotes.ChangeQuote
{
    public class ChangeQuoteCommand : Command<Guid>
    {
        public Guid QuoteId { get; init; }
        public ProductDto Product { get; init; }

        public ChangeQuoteCommand(Guid quoteId, ProductDto product)
        {
            QuoteId = quoteId;
            Product = product;
        }

        public override ValidationResult Validate()
        {
            return new ChangeQuoteCommandValidator().Validate(this);
        }
    }

    public class ChangeQuoteCommandValidator : AbstractValidator<ChangeQuoteCommand>
    {
        public ChangeQuoteCommandValidator()
        {
            RuleFor(x => x.QuoteId).NotEqual(Guid.Empty).WithMessage("QuoteId is empty.");
            RuleFor(x => x.Product).NotNull().WithMessage("Product is empty.");
        }
    }
}