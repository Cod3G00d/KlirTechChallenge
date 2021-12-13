﻿using KlirTechChallenge.Application.Core.CQRS.CommandHandling;

namespace KlirTechChallenge.Application.Promotion.ChangePromotion;

public record class ChangePromotionCommand : Command<Guid>
{
    public Guid PromotionId { get; init; }
    public string Name { get; init; }
    public bool Active { get; init; }


    public ChangePromotionCommand(Guid promotionId, string name, bool active)
    {
        PromotionId = promotionId;
        Name = name;
        Active = active;
    }

    public override ValidationResult Validate()
    {
        return new ChangePromotionCommandValidator().Validate(this);
    }

}

public class ChangePromotionCommandValidator : AbstractValidator<ChangePromotionCommand>
{
    public ChangePromotionCommandValidator()
    {
        
    }
}