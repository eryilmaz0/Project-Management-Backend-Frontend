using System.Collections.Generic;
using System.Security.Claims;
using JiraProject.Entities.Entities;

namespace JiraProject.Entities.Extensions
{
    public static class ClaimExtensions
    {
        public static void AddRoles(this List<Claim> claims, List<Role> roles)
        {
            roles.ForEach(role=> claims.Add(new Claim(ClaimTypes.Role, role.RoleName)));
        }
    }
}