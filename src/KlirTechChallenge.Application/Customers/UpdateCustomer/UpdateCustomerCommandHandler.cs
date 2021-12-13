using System;
using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Application.Core.CQRS.CommandHandling;
using KlirTechChallenge.Application.Core.ExceptionHandling;

namespace KlirTechChallenge.Application.Customers.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : CommandHandler<UpdateCustomerCommand, Guid>
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(IEcommerceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task<Guid> ExecuteCommand(UpdateCustomerCommand request, 
            CancellationToken cancellationToken)
        {
            var customerId = CustomerId.Of(request.CustomerId);
            var customer = await _unitOfWork.Customers
                .GetById(customerId, cancellationToken);

            if (customer == null)
                throw new ApplicationDataException("Customer not found.");

            customer.SetName(request.Name);
            await _unitOfWork.CommitAsync();

            return customer.Id.Value;
        }
    }
}
