using System;
using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain;
using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Application.Core.CQRS.CommandHandling;
using KlirTechChallenge.Application.Core.ExceptionHandling;
using KlirTechChallenge.Domain.Promotion.Rule;

namespace KlirTechChallenge.Application.Quotes.ChangeQuote
{
    public class ChangeQuoteCommandHandler : CommandHandler<ChangeQuoteCommand, Guid>
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;
        private readonly IApllyPromotionBusinessRule _apllyPromotionBusinessRule;

        public ChangeQuoteCommandHandler(
            IEcommerceUnitOfWork unitOfWork,
            IApllyPromotionBusinessRule apllyPromotionBusinessRule)
        {
            _unitOfWork = unitOfWork;
            _apllyPromotionBusinessRule = apllyPromotionBusinessRule;
        }

        public override async Task<Guid> ExecuteCommand(ChangeQuoteCommand command,
            CancellationToken cancellationToken)
        {

            var quoteId = QuoteId.Of(command.QuoteId);
            var quote = await _unitOfWork.Quotes.
                GetById(quoteId, cancellationToken);

            var productId = ProductId.Of(command.Product.Id);

            var product = await _unitOfWork.Products
                .GetById(productId, cancellationToken);

            var promotion = await _unitOfWork.Promotions.
                GetById(product.PromotionId, cancellationToken);

            string promotionName = "";
            if (promotion != null)
            {
                promotion.ChangeItem(promotion.Active);
                promotionName = promotion.Name;
            }

            if (quote == null)
                throw new ApplicationDataException("Quote not found.");

            if (product == null)
                throw new ApplicationDataException("Product not found.");

            var quantity = command.Product.Quantity;
            var quotetemProductData = new QuoteItemProductData(
                product.Id,
                product.Price,
                quantity,
                promotionName,
                0

            );

            _apllyPromotionBusinessRule.ApplyPromotion(quotetemProductData);


            quote.ChangeItem(quotetemProductData);

            await _unitOfWork.CommitAsync();
            return quote.Id.Value;
        }
    }
}