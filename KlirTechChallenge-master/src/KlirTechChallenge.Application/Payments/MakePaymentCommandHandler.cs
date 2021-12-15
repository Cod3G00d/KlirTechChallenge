using System;
using System.Threading;
using KlirTechChallenge.Domain;
using System.Threading.Tasks;
using KlirTechChallenge.Domain.Payments;
using KlirTechChallenge.Application.Core.CQRS.CommandHandling;
using KlirTechChallenge.Application.Core.ExceptionHandling;

namespace KlirTechChallenge.Application.Payments
{
    public class MakePaymentCommandHandler : CommandHandler<MakePaymentCommand, Guid>
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;

        public MakePaymentCommandHandler(
            IEcommerceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<Guid> ExecuteCommand(MakePaymentCommand command,
            CancellationToken cancellationToken)
        {
            var paymentId = PaymentId.Of(command.PaymentId);
            var payment = await _unitOfWork.Payments
                .GetById(paymentId, cancellationToken);

            if (payment == null)
                throw new ApplicationDataException("Payment not found.");

            if (payment.OrderId.Value == Guid.Empty)
                throw new ApplicationDataException("Order not found.");

            try
            {
                // Making a fake payment
                // (here we could call a financial institution service and use the customer billing info)
                var paymentSuccess = MakePayment(payment);

                if (paymentSuccess)
                    payment.MarkAsPaid();
            }
            catch (Exception)
            {
                throw;
            }

            return payment.Id.Value;
        }

        /// <summary>
        /// A simple mock that could be replaced by a real payment API call
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="failRandomly">if true, it may simulate fail / not authorized</param>
        /// <returns></returns>
        private bool MakePayment(Payment payment, bool failRandomly = false)
        {
            bool paymentResult = true;

            if (failRandomly)
            {
                var randomResult = new Random().Next(100) % 2 == 0;
                paymentResult = !randomResult
                    ? throw new ApplicationDataException($"Payment {payment.Id} not authorized.")
                    : randomResult;
            }

            return paymentResult;
        }
    }
}