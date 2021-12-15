using System.Collections.Generic;
using KlirTechChallenge.Application.Orders.GetOrderDetails;
using KlirTechChallenge.Application.Core.CQRS.QueryHandling;
using System;
using FluentValidation.Results;
using FluentValidation;

namespace KlirTechChallenge.Application.Orders.GetOrders
{
    public  class GetOrdersQuery : Query<List<OrderDetailsViewModel>>
    {
        public Guid CustomerId { get; init; }

        public GetOrdersQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public override ValidationResult Validate()
        {
            return new GetOrdersQueryValidator().Validate(this);
        }
    }

    public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
    {
        public GetOrdersQueryValidator()
        {
            RuleFor(x => x.CustomerId).NotEqual(Guid.Empty).WithMessage("CustomerId is empty.");
        }
    }
}