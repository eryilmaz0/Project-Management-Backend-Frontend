using System.Collections.Generic;
using JiraProject.Entities.Entities;

namespace JiraProject.DataAccess.Abstract
{
    public interface IRoleRepository : IRepository<Role>
    {
        IList<Role> GetUserRoles(User user);
        bool IsUserInRole(User user, string role);
    }
}