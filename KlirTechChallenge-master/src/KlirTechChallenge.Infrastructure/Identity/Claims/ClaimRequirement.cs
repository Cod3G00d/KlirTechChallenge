using Microsoft.AspNetCore.Authorization;

namespace KlirTechChallenge.Infrastructure.Identity.Claims;

public record class ClaimRequirement : IAuthorizationRequirement
{
    public ClaimRequirement(string claimName, string claimValue)
    {
        ClaimName = claimName;
        ClaimValue = claimValue;
    }

    public string ClaimName { get; set; }
    public string ClaimValue { get; set; }
}