using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using JiraProject.Business.Abstract;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.DataAccess.Abstract;
using JiraProject.Entities.DataTransferObjects.Response;
using JiraProject.Entities.Entities;

namespace JiraProject.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }



        public IBusinessResult IsUserExist(int userId)
        {
            if (_userRepository.Get(x => x.Id == userId) == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();;
        }




        public IBusinessDataResult<User> GetUser(int userId)
        {
            return new SuccessDataResult<User>(_userRepository.Get(x => x.Id == userId));
        }



        public IBusinessDataResult<ICollection<UserListResponse>> GetUsersOutOfTheProject(int projectId)
        {
            var users = _userRepository.GetAll((x => x.Projects.FirstOrDefault(x => x.Id == projectId) == null), x=>x.Projects, x=>x.Roles);
            var developers = users.Where(x => x.Roles.FirstOrDefault(x => x.RoleName.ToLower() == "leader") == null).ToList();

            var mappedDevelopers = _mapper.Map<List<UserListResponse>>(developers);
            return new SuccessDataResult<ICollection<UserListResponse>>(mappedDevelopers);
        }
    }
}