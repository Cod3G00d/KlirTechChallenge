using KlirTechChallenge.Domain.SeedWork;
using KlirTechChallenge.Domain.Orders;
using KlirTechChallenge.Domain.Quotes;
using KlirTechChallenge.Domain.Payments;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Domain.Customers;
using KlirTechChallenge.Domain.Core.Events;
using KlirTechChallenge.Domain.Promotions;

namespace KlirTechChallenge.Domain
{
    public interface IEcommerceUnitOfWork : IUnitOfWork
    {
        ICustomers Customers { get; }
        IOrders Orders { get; }
        IProducts Products { get; }
        IQuotes Quotes { get; }
        IPayments Payments { get; }
        IStoredEvents StoredEvents { get; }
        IPromotions Promotions { get; }
    }
}