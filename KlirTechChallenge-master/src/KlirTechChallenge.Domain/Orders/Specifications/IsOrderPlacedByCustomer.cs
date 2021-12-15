using System;
using System.Linq.Expressions;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.SeedWork;

namespace KlirTechChallenge.Domain.Orders.Specifications
{
    public class IsOrderPlacedByCustomer : Specification<Order>
    {
        private readonly CustomerId _customerId;

        public IsOrderPlacedByCustomer(CustomerId customerId)
        {
            _customerId = customerId;
        }

        public override Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.CustomerId == _customerId;
        }
    }
}