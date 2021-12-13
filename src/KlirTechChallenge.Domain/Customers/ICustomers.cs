using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain.SeedWork;

namespace KlirTechChallenge.Domain.Customers;

public interface ICustomers : IRepository<Customer>
{
    Task Add(Customer customer, CancellationToken cancellationToken = default);
    Task<Customer> GetById(CustomerId id, CancellationToken cancellationToken = default);
    Task<Customer> GetByEmail(string email, CancellationToken cancellationToken = default);
}
