using System;
using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.Quotes;
using System.Linq;
using System.Collections.Generic;
using KlirTechChallenge.Domain.SharedKernel;
using KlirTechChallenge.Domain.Orders;
using KlirTechChallenge.Application.Core.CQRS.CommandHandling;
using KlirTechChallenge.Application.Core.ExceptionHandling;

namespace KlirTechChallenge.Application.Orders.PlaceOrder
{
    public class PlaceOrderCommandHandler : CommandHandler<PlaceOrderCommand, Guid>
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;
        private readonly ICurrencyConverter _currencyConverter;        

        public PlaceOrderCommandHandler(
            IEcommerceUnitOfWork unitOfWork,
            ICurrencyConverter converter)
        {
            _unitOfWork = unitOfWork;
            _currencyConverter = converter;
        }

        public override async Task<Guid> ExecuteCommand(PlaceOrderCommand command, 
            CancellationToken cancellationToken)
        {
            var customerId = CustomerId.Of(command.CustomerId);
            var productsData = new List<QuoteItemProductData>();
            var quoteId = QuoteId.Of(command.QuoteId);
            var quote = await _unitOfWork.Quotes
                .GetById(quoteId, cancellationToken);            
            var customer = await _unitOfWork.Customers
                .GetById(customerId, cancellationToken);

            if (customer == null)
                throw new ApplicationDataException("Customer not found.");

            if (quote == null)
                throw new ApplicationDataException("Quote not found.");

            var currency = Currency.FromCode(command.Currency);

            var products = await _unitOfWork.Products
                .GetByIds(quote.Items.Select(i => i.ProductId).ToList());

            if (products == null)
                throw new ApplicationDataException("Products couldn't be loaded.");

            foreach (var item in quote.Items)
            {
                var product = products
                    .Where(p => p.Id == item.ProductId)
                    .FirstOrDefault();

                var promotion = await _unitOfWork.Promotions
                   .GetById(product.PromotionId, cancellationToken);

                string promotionName = "";

                if(promotion != null)
                {
                    promotionName = promotion.Name;
                }


                productsData.Add(
                    new QuoteItemProductData(product.Id, product.Price, item.Quantity, promotionName, Convert.ToDecimal(item.TotalPrice))
                );
            }

            var order = Order.PlaceOrder(customerId, quoteId, productsData, currency, _currencyConverter);
            await _unitOfWork.Orders.Add(order);
            await _unitOfWork.CommitAsync();
            return order.Id.Value;
        }
    }
}
