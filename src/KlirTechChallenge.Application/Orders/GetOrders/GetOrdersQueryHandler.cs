﻿using System.Linq;
using System.Threading;
using KlirTechChallenge.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.SharedKernel;
using KlirTechChallenge.Domain.Orders.Specifications;
using KlirTechChallenge.Application.Core.CQRS.QueryHandling;
using KlirTechChallenge.Application.Core.ExceptionHandling;
using KlirTechChallenge.Application.Orders.GetOrderDetails;

namespace KlirTechChallenge.Application.Orders.GetOrders;

public class GetOrdersQueryHandler : QueryHandler<GetOrdersQuery, 
    List<OrderDetailsViewModel>>
{
    private readonly IEcommerceUnitOfWork _unitOfWork;

    public GetOrdersQueryHandler(
        IEcommerceUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public override async Task<List<OrderDetailsViewModel>> ExecuteQuery(GetOrdersQuery query, 
        CancellationToken cancellationToken)
    {
        List<OrderDetailsViewModel> viewModelList = new List<OrderDetailsViewModel>();

        var customerId = CustomerId.Of(query.CustomerId);
        var customer = await _unitOfWork.Customers
            .GetById(customerId, cancellationToken);

        if (customer == null)
            throw new ApplicationDataException("Custumer not found.");

        var isOrderPlacedByCustomer = new IsOrderPlacedByCustomer(customer.Id);
        var customerOrders = await _unitOfWork.Orders.Find(isOrderPlacedByCustomer);

        foreach (var order in customerOrders)
        {
            var productIds = order.OrderLines.
                Select(p => p.ProductId).ToList();

            var products = await _unitOfWork.Products
                .GetByIds(productIds, cancellationToken);

            OrderDetailsViewModel viewModel = new OrderDetailsViewModel();
            viewModel.OrderId = order.Id.Value;
            viewModel.CreatedAt = order.CreatedAt.ToString();
            viewModel.Status = OrderStatusPrettier.Prettify(order.Status);

            foreach (var orderLine in order.OrderLines)
            {
                var product = products.Single(
                    (System.Func<Domain.Products.Product, bool>)
                    (p => p.Id == orderLine.ProductId));

                var currency = Currency
                    .FromCode(orderLine.ProductExchangePrice.CurrencyCode);

                viewModel.OrderLines.Add(new OrderLinesDetailsViewModel
                {
                    ProductId = orderLine.ProductId.Value,
                    ProductQuantity = orderLine.Quantity,
                    ProductPrice = orderLine.ProductExchangePrice.Value,
                    ProductName = product.Name,
                    CurrencySymbol = currency.Symbol,
                });
            }

            viewModel.CalculateTotalOrderPrice(Convert.ToDouble(order.TotalPrice.Value));
            viewModelList.Add(viewModel);
        }

        return viewModelList;
    }        
}