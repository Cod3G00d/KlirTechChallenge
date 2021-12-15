using MediatR;
using System.Threading;
using KlirTechChallenge.Domain;
using System.Threading.Tasks;
using KlirTechChallenge.Domain.Orders;
using KlirTechChallenge.Domain.Payments.Events;
using KlirTechChallenge.Application.Core.ExceptionHandling;

namespace KlirTechChallenge.Application.Orders.PlaceOrder
{
    public class PaymentAuthorizedEventHandler : INotificationHandler<PaymentAuthorizedEvent>
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;
        private readonly IOrderStatusWorkflow _orderStatusWorkflow;
        private readonly IOrderStatusBroadcaster _orderStatusBroadcaster;

        public PaymentAuthorizedEventHandler(
            IEcommerceUnitOfWork unitOfWork,
            IOrderStatusWorkflow orderStatusWorkflow,
            IOrderStatusBroadcaster orderStatusBroadcaster)
        {
            _unitOfWork = unitOfWork;
            _orderStatusWorkflow = orderStatusWorkflow;
            _orderStatusBroadcaster = orderStatusBroadcaster;
        }

        public async Task Handle(PaymentAuthorizedEvent paymentAuthorizedEvent,
            CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.Payments
                .GetById(paymentAuthorizedEvent.PaymentId, cancellationToken);

            var order = await _unitOfWork.Orders
                .GetById(payment.OrderId, cancellationToken);

            if (payment == null)
                throw new ApplicationDataException("Payment not found.");

            if (order == null)
                throw new ApplicationDataException("Order not found.");

            // Changing order status
            _orderStatusWorkflow.CalculateOrderStatus(order, payment);
            await _unitOfWork.CommitAsync(cancellationToken);

            // Broadcasting order update
            await _orderStatusBroadcaster.BroadcastOrderStatus(
                order.CustomerId,
                order.Id,
                order.Status
            );
        }
    }
}