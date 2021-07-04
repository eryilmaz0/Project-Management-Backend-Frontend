using System.Collections.Generic;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.Entities.DataTransferObjects.Response;
using JiraProject.Entities.Entities;

namespace JiraProject.Business.Abstract
{
    public interface IUserService
    {
        IBusinessResult IsUserExist(int userId);
        IBusinessDataResult<User> GetUser(int userId);
        IBusinessDataResult<ICollection<UserListResponse>> GetUsersOutOfTheProject(int projectId);
    }
}