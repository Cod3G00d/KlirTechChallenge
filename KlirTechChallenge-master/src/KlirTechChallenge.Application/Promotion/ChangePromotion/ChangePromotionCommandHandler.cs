using System;
using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain;
using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Application.Core.CQRS.CommandHandling;
using KlirTechChallenge.Application.Core.ExceptionHandling;
using KlirTechChallenge.Domain.Promotion.Rule;
using KlirTechChallenge.Application.Promotion.ChangePromotion;
using KlirTechChallenge.Domain.Promotions;

namespace KlirTechChallenge.Application.Promotions.ChangePromotion;

public class ChangePromotionCommandHandler : CommandHandler<ChangePromotionCommand, Guid>
{
    private readonly IEcommerceUnitOfWork _unitOfWork;
    

    public ChangePromotionCommandHandler(
        IEcommerceUnitOfWork unitOfWork
       )
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task<Guid> ExecuteCommand(ChangePromotionCommand command, 
        CancellationToken cancellationToken)
    {
        var promotionId = PromotionId.Of(command.PromotionId);
        var promotion = await _unitOfWork.Promotions.
            GetById(promotionId, cancellationToken);

        if (promotion == null)
            throw new ApplicationDataException("Promotion not found.");

        promotion.ChangeItem(command.Active);
            
        await _unitOfWork.CommitAsync();
      
        return promotion.Id.Value;
    }
}
