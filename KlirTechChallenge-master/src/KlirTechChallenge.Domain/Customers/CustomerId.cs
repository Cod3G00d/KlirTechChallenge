using KlirTechChallenge.Domain.SeedWork;
using System;

namespace KlirTechChallenge.Domain.Customers
{
    public class CustomerId : StronglyTypedId<CustomerId>
    {
        public CustomerId(Guid value) : base(value)
        {
        }

        public static CustomerId Of(Guid customerId)
        {
            if (customerId == Guid.Empty)
                throw new BusinessRuleException("Customer Id must be provided.");

            return new CustomerId(customerId);
        }
    }
}