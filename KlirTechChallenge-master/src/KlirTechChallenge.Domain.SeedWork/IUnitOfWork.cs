using System.Threading;
using System.Threading.Tasks;

namespace KlirTechChallenge.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}