using System.Threading.Tasks;

namespace KlirTechChallenge.Infrastructure.Identity.Services
{
    public interface IJwtService
    {
        Task<string> GenerateJwt(string email);
    }
}