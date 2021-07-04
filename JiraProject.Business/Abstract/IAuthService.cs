using JiraProject.Business.BusinessResultObjects;
using JiraProject.Business.Security.JWT;
using JiraProject.Entities.DataTransferObjects.Request;
using JiraProject.Entities.Entities;

namespace JiraProject.Business.Abstract
{
    public interface IAuthService
    {
        IBusinessResult Register(RegisterRequest registerRequest);
        IBusinessDataResult<AccessToken> Login(LoginRequest loginRequest);
        IBusinessDataResult<User> GetAuthenticatedUser();
    }
}