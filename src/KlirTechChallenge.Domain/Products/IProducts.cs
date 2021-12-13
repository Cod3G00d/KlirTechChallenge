using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using KlirTechChallenge.Domain.SeedWork;

namespace KlirTechChallenge.Domain.Products;

public interface IProducts : IRepository<Product>
{
    Task Add(Product product, CancellationToken cancellationToken = default);
    Task AddList(List<Product> products, CancellationToken cancellationToken = default);
    Task<Product> GetById(ProductId id, CancellationToken cancellationToken = default);
    Task<List<Product>> GetByIds(List<ProductId> ids, CancellationToken cancellationToken = default);
    Task<List<Product>> ListAll(CancellationToken cancellationToken = default);
}