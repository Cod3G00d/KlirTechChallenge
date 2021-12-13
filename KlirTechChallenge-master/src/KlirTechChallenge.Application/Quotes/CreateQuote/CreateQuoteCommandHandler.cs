using System;
using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain;
using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Application.Core.ExceptionHandling;
using KlirTechChallenge.Application.Core.CQRS.CommandHandling;
using KlirTechChallenge.Domain.Promotion.Rule;

namespace KlirTechChallenge.Application.Quotes.SaveQuote;

public class CreateQuoteCommandHandler : CommandHandler<CreateQuoteCommand, Guid>
{
    private readonly IEcommerceUnitOfWork _unitOfWork;
    private readonly IApllyPromotionBusinessRule _apllyPromotionBusinessRule;


    public CreateQuoteCommandHandler(
        IEcommerceUnitOfWork unitOfWork,
        IApllyPromotionBusinessRule apllyPromotionBusinessRule)
    {
        _unitOfWork = unitOfWork;
        _apllyPromotionBusinessRule = apllyPromotionBusinessRule;
    }

    public override async Task<Guid> ExecuteCommand(CreateQuoteCommand command, 
        CancellationToken cancellationToken)
    {
        var customerId = CustomerId.Of(command.CustomerId);
        var customer = await _unitOfWork.Customers
            .GetById(customerId, cancellationToken);

        var productId = ProductId.Of(command.Product.Id);
        var product = await _unitOfWork.Products
            .GetById(productId, cancellationToken);

        var promotion = await _unitOfWork.Promotions
           .GetById(product.PromotionId, cancellationToken);

        if (customer == null)
            throw new ApplicationDataException("Customer not found.");

        if (product == null)
            throw new ApplicationDataException("Product not found.");

        var quantity = command.Product.Quantity;

        var quotetemProductData = new QuoteItemProductData(
            product.Id, 
            product.Price, 
            quantity,
            promotion.Name,
            0
        );

        _apllyPromotionBusinessRule.ApplyPromotion(quotetemProductData);

        var quote = Quote.CreateNew(customerId);
        quote.AddItem(quotetemProductData);

        await _unitOfWork.Quotes
            .Add(quote, cancellationToken);
   
        await _unitOfWork.CommitAsync();
        return quote.Id.Value;
    }
}