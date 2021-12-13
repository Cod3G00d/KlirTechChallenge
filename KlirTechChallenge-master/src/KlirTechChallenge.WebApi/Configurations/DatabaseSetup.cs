using KlirTechChallenge.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace KlirTechChallenge.WebApi.Configurations;

public static class DatabaseSetup
{
    public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        string connString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<KlirTechChallengeContext>(options =>
        {
            options.UseSqlServer(connString,
            sqlServerOptionsAction: sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            });
        });

        services.AddDbContextPool<IdentityContext>(options =>
        {
            options.UseSqlServer(connString);
        });
    }
}