using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using KlirTechChallenge.Domain.SeedWork;

namespace KlirTechChallenge.Domain.Promotions
{
    public interface IPromotions : IRepository<Promotion>
    {
        Task Add(Promotion promotion, CancellationToken cancellationToken = default);
        Task AddList(List<Promotion> promotion, CancellationToken cancellationToken = default);
        Task<Promotion> GetById(PromotionId id, CancellationToken cancellationToken = default);
        Task<List<Promotion>> GetByIds(List<PromotionId> ids, CancellationToken cancellationToken = default);
        Task<List<Promotion>> ListAll(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Promotion>> Find(Specification<Promotion> specification, CancellationToken cancellationToken = default);

    }
}