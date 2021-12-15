using System;
using System.Linq.Expressions;
using KlirTechChallenge.Domain.Orders;
using KlirTechChallenge.Domain.SeedWork;

namespace KlirTechChallenge.Domain.Payments.Specifications
{
    public class IsPaymentOfOrder : Specification<Payment>
    {
        private readonly OrderId _orderId;

        public IsPaymentOfOrder(OrderId orderId)
        {
            _orderId = orderId;
        }

        public override Expression<Func<Payment, bool>> ToExpression()
        {
            return payment => payment.OrderId == _orderId;
        }
    }
}