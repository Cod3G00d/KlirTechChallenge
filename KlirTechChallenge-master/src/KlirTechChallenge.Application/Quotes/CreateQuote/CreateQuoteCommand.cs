﻿using FluentValidation;
using FluentValidation.Results;
using KlirTechChallenge.Application.Core.CQRS.CommandHandling;
using System;

namespace KlirTechChallenge.Application.Quotes.SaveQuote
{
    public  class CreateQuoteCommand : Command<Guid>
    {
        public Guid CustomerId { get; init; }
        public ProductDto Product { get; init; }

        public CreateQuoteCommand(Guid customerId, ProductDto product)
        {
            CustomerId = customerId;
            Product = product;
        }

        public override ValidationResult Validate()
        {
            return new CreateQuoteCommandValidator().Validate(this);
        }
    }

    public class CreateQuoteCommandValidator : AbstractValidator<CreateQuoteCommand>
    {
        public CreateQuoteCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEqual(Guid.Empty).WithMessage("CustomerId is empty.");
            RuleFor(x => x.Product).NotNull().WithMessage("Product is empty.");
        }
    }
}