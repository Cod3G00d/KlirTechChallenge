using KlirTechChallenge.Domain.SeedWork;
using System;

namespace KlirTechChallenge.Domain.Promotions
{
    public class PromotionId : StronglyTypedId<PromotionId>
    {
        public PromotionId(Guid value) : base(value)
        {
        }

        public static PromotionId Of(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new BusinessRuleException("Product Id must be provided.");

            return new PromotionId(productId);
        }
    }
}