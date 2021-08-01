using System.Collections.Generic;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.DataAccess.Concrete.EntityFramework;
using JiraProject.Entities.DataTransferObjects.Request;
using JiraProject.Entities.DataTransferObjects.Response;
using JiraProject.Entities.Entities;

namespace JiraProject.Business.Abstract
{
    public interface IProjectService
    {
        IBusinessDataResult<ICollection<ProjectListResponse>> GetProjectsByDepartment(int departmentId);
        IBusinessDataResult<ICollection<ProjectListResponse>> GetProjects();
        IBusinessDataResult<ICollection<ProjectListResponse>> GetProjectsByFilter(string filter);
        IBusinessDataResult<ProjectDetailResponse> GetProjectDetail(int projectId);
        IBusinessResult CreateProject(CreateProjectRequest request);
        IBusinessResult EditProject(EditProjectRequest request);
        IBusinessResult ActivateProject(int projectId);
        IBusinessResult DeactivateProject(int projectId);
        IBusinessResult IsProjectExist(int projectId);
        IBusinessResult IsProjectActive(int projectId);
        IBusinessResult IsUserProjectLeader(int projectId, User user);
        IBusinessDataResult<ICollection<UserListResponse>> GetProjectMembers(int projectId);
        IBusinessResult AddUserToProject(AddUserToProjectRequest request);
        IBusinessResult RemoveUserFromProject(RemoveUserFromProjectRequest request);
        IBusinessResult UserIsMemberOfProject(User user, int projectId);
        IBusinessDataResult<ICollection<UserListResponse>> GetUsersOutOfTheProject(int projectId);

    }
}