using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain;
using KlirTechChallenge.Domain.SharedKernel;
using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Application.Core.CQRS.QueryHandling;
using KlirTechChallenge.Application.Core.ExceptionHandling;
using KlirTechChallenge.Domain.Promotion.Rule;

namespace KlirTechChallenge.Application.Quotes.GetQuoteDetails;

public class GetQuoteQueryHandler : QueryHandler<GetQuoteDetailsQuery, QuoteDetailsViewModel>
{
    private readonly IEcommerceUnitOfWork _unitOfWork;
    private readonly ICurrencyConverter _currencyConverter;
    private readonly IApllyPromotionBusinessRule _apllyPromotionBusinessRule;

    public GetQuoteQueryHandler(IEcommerceUnitOfWork unitOfWork, ICurrencyConverter currencyConverter, IApllyPromotionBusinessRule apllyPromotionBusinessRule)
    {
        _unitOfWork = unitOfWork;
        _currencyConverter = currencyConverter;
        _apllyPromotionBusinessRule = apllyPromotionBusinessRule;
    }

    public async override Task<QuoteDetailsViewModel> ExecuteQuery(GetQuoteDetailsQuery query, 
        CancellationToken cancellationToken)
    {
        QuoteDetailsViewModel viewModel = new QuoteDetailsViewModel();

        var quoteId = QuoteId.Of(query.QuoteId);
        var quote = await _unitOfWork.Quotes
            .GetById(quoteId, cancellationToken);

        if (quote == null)
            throw new ApplicationDataException("Quote not found.");

        if (string.IsNullOrWhiteSpace(query.Currency))
            throw new ApplicationDataException("Currency can't be empty.");

        if (quote.Items.Count > 0)
        {
            viewModel.QuoteId = quote.Id.Value;
            var currency = Currency.FromCode(query.Currency);
            var productIds = quote.Items.Select(p => p.ProductId).ToList();
            var products = await _unitOfWork.Products
                .GetByIds(productIds, cancellationToken);
            
            if (products == null)
                throw new ApplicationDataException("Products not found");

            foreach (var quoteItem in quote.Items)
            {
                var product = products.Single(p => p.Id == quoteItem.ProductId);

                var promotion = await _unitOfWork.Promotions
                 .GetById(product.PromotionId, cancellationToken);

                string promotionName = "";

                if(promotion != null)
                {
                    promotionName = promotion.Name;
                }

                var convertedPrice = _currencyConverter.Convert(currency, product.Price);
                viewModel.QuoteItems.Add(new QuoteItemDetailsViewModel
                {                        
                    ProductId = quoteItem.ProductId.Value,
                    ProductQuantity = quoteItem.Quantity,
                    ProductName = product.Name,
                    ProductPrice = Math.Round(convertedPrice.Value, 2),
                    CurrencySymbol = currency.Symbol,
                    PromotionName = promotionName,
                    TotalProductPrice = Math.Round(quoteItem.TotalPrice, 2)
                });
            }

            viewModel.CalculateTotalOrderPrice();
        }

        return viewModel;
    }
}