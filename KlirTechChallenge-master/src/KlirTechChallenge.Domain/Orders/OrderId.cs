using KlirTechChallenge.Domain.SeedWork;
using System;

namespace KlirTechChallenge.Domain.Orders
{
    public class OrderId : StronglyTypedId<OrderId>
    {
        public OrderId(Guid value) : base(value)
        {
        }

        public static OrderId Of(Guid orderId)
        {
            if (orderId == Guid.Empty)
                throw new BusinessRuleException("Order Id must be provided.");

            return new OrderId(orderId);
        }
    }
}