using System;
using System.Threading;
using KlirTechChallenge.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using KlirTechChallenge.Domain.SharedKernel;
using KlirTechChallenge.Application.Promotions;
using KlirTechChallenge.Application.Core.CQRS.QueryHandling;
using KlirTechChallenge.Application.Core.ExceptionHandling;
using KlirTechChallenge.Application.Promotion.Promotions;

namespace KlirTechChallenge.Application.Customers.ListCustomerEventHistory;

public class ListPromotionsQueryHandler : QueryHandler<ListPromotionsQuery, IList<PromotionsViewModel>> 
{
    private readonly IEcommerceUnitOfWork _unitOfWork;

    public ListPromotionsQueryHandler(
        IEcommerceUnitOfWork unitOfWork,
        ICurrencyConverter currencyConverter)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task<IList<PromotionsViewModel>> ExecuteQuery(ListPromotionsQuery query, 
        CancellationToken cancellationToken)
    {
        IList<PromotionsViewModel> PromotionsViewModel = new List<PromotionsViewModel>();
        var Promotions = await _unitOfWork.Promotions
            .ListAll(cancellationToken);


        foreach (var Promotion in Promotions)
        {
           
            PromotionsViewModel.Add(new PromotionsViewModel
            {
                Id = Promotion.Id.Value,
                Name = Promotion.Name,
                Active = Promotion.Active
            });
        }

        return PromotionsViewModel;
    }
}
