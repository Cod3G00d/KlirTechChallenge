using KlirTechChallenge.Domain;
using KlirTechChallenge.Domain.Customers;

namespace KlirTechChallenge.Application.Customers.DomainServices
{
    public class CustomerUniquenessChecker : ICustomerUniquenessChecker
    {
        private readonly IEcommerceUnitOfWork _unitOfWork;

        public CustomerUniquenessChecker(IEcommerceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsUserUnique(string customerEmail)
        {
            var customer = _unitOfWork.Customers
                .GetByEmail(customerEmail).Result;

            return customer == null;
        }
    }
}