using MediatR;
using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain;
using KlirTechChallenge.Domain.Payments;
using KlirTechChallenge.Domain.Orders.Events;
using KlirTechChallenge.Application.Core.ExceptionHandling;

namespace KlirTechChallenge.Application.Orders.PlaceOrder
{
    public class OrderPlacedEventHandler : INotificationHandler<OrderPlacedEvent>
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;

        public OrderPlacedEventHandler(IEcommerceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(OrderPlacedEvent orderPlacedEvent, 
            CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders
                .GetById(orderPlacedEvent.OrderId, cancellationToken);

            if (order == null)
                throw new ApplicationDataException("Order not found.");

            // Creating a payment
            var payment = Payment
                .CreateNew(order.CustomerId, order.Id);

            await _unitOfWork.Payments
                .Add(payment);

            await _unitOfWork.CommitAsync();            
        }
    }
}
