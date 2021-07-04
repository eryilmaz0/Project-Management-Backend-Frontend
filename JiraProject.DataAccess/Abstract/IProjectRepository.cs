using System.Collections.Generic;
using JiraProject.Entities.Entities;

namespace JiraProject.DataAccess.Abstract
{
    public interface IProjectRepository : IRepository<Project>
    {
        void RemoveUserFromProject(int userId, int projectId);
        void AddUserToProject(User user, int projectId);
    }
}