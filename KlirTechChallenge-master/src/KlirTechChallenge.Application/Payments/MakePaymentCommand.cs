using FluentValidation;
using FluentValidation.Results;
using KlirTechChallenge.Application.Core.CQRS.CommandHandling;
using System;

namespace KlirTechChallenge.Application.Payments
{
    public  class MakePaymentCommand : Command<Guid>
    {
        public Guid PaymentId { get; init; }

        public MakePaymentCommand(Guid paymentId)
        {
            PaymentId = paymentId;
        }

        public override ValidationResult Validate()
        {
            return new MakePaymentCommandValidator().Validate(this);
        }
    }

    public class MakePaymentCommandValidator : AbstractValidator<MakePaymentCommand>
    {
        public MakePaymentCommandValidator()
        {
            RuleFor(x => x.PaymentId).NotEqual(Guid.Empty).WithMessage("PaymentId is empty.");
        }
    }
}