using System;

namespace KlirTechChallenge.Application.Promotion.ChangePromotion
{
    public class ChangePromotionRequest
    {
        public Guid PromotionId { get; init; }
        public string Name { get; init; }
        public bool Active { get; init; }
    }
}