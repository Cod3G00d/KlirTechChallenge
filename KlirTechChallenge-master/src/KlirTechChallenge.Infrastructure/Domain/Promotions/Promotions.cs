using System.Linq;
using System.Collections.Generic;
using KlirTechChallenge.Domain.Payments;
using KlirTechChallenge.Infrastructure.Database.Context;
using System;
using System.Threading;
using System.Threading.Tasks;
using KlirTechChallenge.Domain.SeedWork;
using KlirTechChallenge.Domain.Promotions;
using Microsoft.EntityFrameworkCore;

namespace KlirTechChallenge.Infrastructure.Domain.Products
{
    public class Promotions : IPromotions
    {
        private readonly KlirTechChallengeContext _context;

        public Promotions(KlirTechChallengeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Promotion promotion, CancellationToken cancellationToken = default)
        {
            await _context.Promotions.AddAsync(promotion, cancellationToken);
        }

        public async Task AddList(List<Promotion> promotion, CancellationToken cancellationToken = default)
        {
            await _context.Promotions.AddRangeAsync(promotion, cancellationToken);
        }

        public async Task<Promotion> GetById(PromotionId id, CancellationToken cancellationToken = default)
        {
            return await _context.Promotions.Where(x => x.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Promotion>> Find(Specification<Promotion> specification, CancellationToken cancellationToken = default)
        {
            return await _context.Promotions
                .Where(specification.ToExpression())
                .ToListAsync();
        }

        public async Task<List<Promotion>> GetByIds(List<PromotionId> ids, CancellationToken cancellationToken = default)
        {
            return await _context.Promotions.Where(x => ids.Contains(x.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Promotion>> ListAll(CancellationToken cancellationToken = default)
        {
            return await _context.Promotions.ToListAsync(cancellationToken);
        }
    }
}