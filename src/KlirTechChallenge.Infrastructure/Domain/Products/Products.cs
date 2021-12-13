using System.Linq;
using System.Collections.Generic;
using KlirTechChallenge.Domain.Products;
using KlirTechChallenge.Infrastructure.Database.Context;

namespace KlirTechChallenge.Infrastructure.Domain.Products;

public class Products : IProducts
{
    private readonly KlirTechChallengeContext _context;

    public Products(KlirTechChallengeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
    }

    public async Task AddList(List<Product> products, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddRangeAsync(products, cancellationToken);
    }

    public async Task<Product> GetById(ProductId id, CancellationToken cancellationToken = default)
    {
        return await _context.Products.Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Product>> GetByIds(List<ProductId> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Products.Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> ListAll(CancellationToken cancellationToken = default)
    {
        return await _context.Products.ToListAsync(cancellationToken); 
    }
}
