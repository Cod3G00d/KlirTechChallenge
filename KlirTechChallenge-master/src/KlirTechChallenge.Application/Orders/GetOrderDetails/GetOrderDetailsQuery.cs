using FluentValidation;
using FluentValidation.Results;
using KlirTechChallenge.Application.Core.CQRS.QueryHandling;
using System;

namespace KlirTechChallenge.Application.Orders.GetOrderDetails
{
    public  class GetOrderDetailsQuery : Query<OrderDetailsViewModel>
    {
        public Guid CustomerId { get; init; }
        public Guid OrderId { get; init; }

        public GetOrderDetailsQuery(Guid customerId, Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }

        public override ValidationResult Validate()
        {
            return new GetOrderDetailsQueryValidator().Validate(this);
        }
    }

    public class GetOrderDetailsQueryValidator : AbstractValidator<GetOrderDetailsQuery>
    {
        public GetOrderDetailsQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotEqual(Guid.Empty).WithMessage("CustomerId is empty.");
            RuleFor(x => x.OrderId).NotEqual(Guid.Empty).WithMessage("OrderId is empty.");
        }
    }
}