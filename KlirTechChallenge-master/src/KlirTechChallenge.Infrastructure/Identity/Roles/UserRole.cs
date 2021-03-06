using Microsoft.AspNetCore.Identity;
using System;

namespace KlirTechChallenge.Infrastructure.Identity.Roles
{
    public class UserRole : IdentityRole<Guid>
    {
        public UserRole()
        {
            this.Id = Guid.NewGuid();
        }

        public UserRole(string name)
            : this()
        {
            this.Name = name;
        }
    }
}