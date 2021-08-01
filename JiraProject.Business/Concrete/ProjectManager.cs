using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using FluentValidation;
using JiraProject.Business.Abstract;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.DataAccess.Abstract;
using JiraProject.DataAccess.Concrete.EntityFramework;
using JiraProject.Entities.DataTransferObjects.Request;
using JiraProject.Entities.DataTransferObjects.Response;
using JiraProject.Entities.DbContext;
using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraProject.Business.Concrete
{
    public class ProjectManager : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IDepartmentService _departmentService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ICacheService _cacheManager;


        public ProjectManager(IProjectRepository projectRepository, IDepartmentService departmentService, IAuthService authService, 
                                IMapper mapper, IUserService userService, ICacheService cacheManager)

        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _departmentService = departmentService;
            _authService = authService;
            _userService = userService;
            _cacheManager = cacheManager;
        }




        public IBusinessDataResult<ICollection<ProjectListResponse>> GetProjectsByDepartment(int departmentId)
        {

            if (!_departmentService.IsDepartmentExist(departmentId).IsSuccess)
            {
                return new ErrorDataResult<ICollection<ProjectListResponse>>("Departman Bulunamadı.");
            }

            var projectsByDepartment = _projectRepository.GetAll(x => x.DepartmentId == departmentId, 
                                        x => x.ProjectLeader, x => x.Department, x=>x.Members, x=>x.Tasks).ToList();

            var mappedProjects = _mapper.Map<List<ProjectListResponse>>(projectsByDepartment);
            return new SuccessDataResult<ICollection<ProjectListResponse>>(mappedProjects);
        }



        public IBusinessDataResult<ICollection<ProjectListResponse>> GetProjects()
        {
            var projectsByDepartment = _projectRepository.GetAll(null, x => x.ProjectLeader, x => x.Department, x => x.Members, x => x.Tasks).OrderByDescending(x=>x.Created).ToList();

            var mappedProjects = _mapper.Map<List<ProjectListResponse>>(projectsByDepartment);
            return new SuccessDataResult<ICollection<ProjectListResponse>>(mappedProjects);
        }




        public IBusinessDataResult<ICollection<ProjectListResponse>> GetProjectsByFilter(string filter)
        {
            var projectsByDepartment = _projectRepository.GetAll(x=>x.ProjectName.ToLower().Contains(filter.ToLower())
                                    || x.ProjectDescription.ToLower().Contains(filter.ToLower()), x => x.ProjectLeader, x => x.Department, x => x.Members, x => x.Tasks).ToList();

            var mappedProjects = _mapper.Map<List<ProjectListResponse>>(projectsByDepartment);
            return new SuccessDataResult<ICollection<ProjectListResponse>>(mappedProjects);
        }



        public IBusinessDataResult<ProjectDetailResponse> GetProjectDetail(int projectId)
        {
            var project = _projectRepository.Get(x=>x.Id == projectId, x=>x.Department, x=>x.ProjectLeader, x=>x.Members, x=>x.Tasks);
            var authenticatedUser = _authService.GetAuthenticatedUser().Data;


            if (project == null)
            {
                return new ErrorDataResult<ProjectDetailResponse>("Proje Bulunamadı.");
            }


            if (project.ProjectLeaderId != authenticatedUser.Id && project.Members.FirstOrDefault(x => x.Id == authenticatedUser.Id) == null)

            {
                return new ErrorDataResult<ProjectDetailResponse>("Üyesi Olmadığınız Projenin Detay Bilgilerini Göremezsiniz.");
            }



            var mappedProject = _mapper.Map<ProjectDetailResponse>(project);
            return new SuccessDataResult<ProjectDetailResponse>(mappedProject);
        }




        public IBusinessResult CreateProject(CreateProjectRequest request)
        {
            bool isDepartmentExist = _departmentService.IsDepartmentExist(request.DepartmentId).IsSuccess;

            if (!isDepartmentExist)
            {
                return new ErrorResult("Departman Bulunamadı.");
            }

            var newProject = _mapper.Map<Project>(request);
            newProject.ProjectLeaderId = _authService.GetAuthenticatedUser().Data.Id;
                
            _projectRepository.Add(newProject);
            _projectRepository.Save();
            
            return new SuccessResult("Proje Oluşturuldu.");
        }




        public IBusinessResult EditProject(EditProjectRequest request)
        {
            var project = _projectRepository.Get(x => x.Id == request.Id);

            if (project == null)
            {
                return new ErrorResult("Proje Bulunamadı.");
            }

            if (!_departmentService.IsDepartmentExist(request.DepartmentId).IsSuccess)
            {
                return new ErrorResult("Departman Bulunamadı.");
            }


            var projectToEdit = _mapper.Map<Project>(request);
            projectToEdit.LastUpdated = DateTime.Now;
            _projectRepository.Update(projectToEdit);
            _projectRepository.Save();

            return new SuccessResult("Proje Güncellendi.");
        }




        public IBusinessResult ActivateProject(int projectId)
        {
            var project = _projectRepository.Get(x => x.Id == projectId);

            if (project == null)
            {
                return new ErrorResult("Proje Bulunamadı.");
            }

            if (project.ProjectLeaderId != _authService.GetAuthenticatedUser().Data.Id)
            {
                return new ErrorResult("Bu Projeyi Açamazsınız.");
            }

            if (project.IsActive == true)
            {
                return new ErrorResult("Proje Zaten Aktif.");
            }

            project.IsActive = true;
            _projectRepository.Update(project);
            _projectRepository.Save();

            return new SuccessResult("Proje Aktifleştirildi.");
        }




        public IBusinessResult DeactivateProject(int projectId)
        {
            var project = _projectRepository.Get(x => x.Id == projectId);


            if (project == null)
            {
                return new ErrorResult("Proje Bulunamadı.");
            }


            if (!project.IsActive)
            {
                return new SuccessResult("Proje Zaten Aktif Değil.");
            }


            if (project.ProjectLeaderId != _authService.GetAuthenticatedUser().Data.Id)
            {
                return new ErrorResult("Bu Projeyi Kapatamazsınız.");
            }


            project.IsActive = false;
            _projectRepository.Update(project);
            _projectRepository.Save();

            return new SuccessResult("Proje Deaktif Edildi.");

        }




        public IBusinessResult IsProjectExist(int projectId)
        {
            var project = _projectRepository.Get(x => x.Id == projectId);

            if (project == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }





        public IBusinessResult IsProjectActive(int projectId)
        {
            var project = _projectRepository.Get(x => x.Id == projectId);

            if (project.IsActive)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }




        public IBusinessResult IsUserProjectLeader(int projectId, User user)
        {
            var project = _projectRepository.Get(x => x.Id == projectId);

            if (project.ProjectLeaderId != user.Id)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }





        public IBusinessDataResult<ICollection<UserListResponse>> GetProjectMembers(int projectId)
        {
            if (_cacheManager.IsKeyExist($"project:{projectId}/members"))
            {
                var projectMembersFromCache = _cacheManager.Get<ICollection<UserListResponse>>($"project:{projectId}/members");
                return new SuccessDataResult<ICollection<UserListResponse>>(projectMembersFromCache);
            }


            var projectMembers = _projectRepository.Get(x => x.Id == projectId, x => x.Members).Members;
            var mappedMembers = _mapper.Map<List<UserListResponse>>(projectMembers);

            _cacheManager.Add($"project:{projectId}/members", mappedMembers, 60);
            return new SuccessDataResult<ICollection<UserListResponse>>(mappedMembers);
        }





        public IBusinessResult AddUserToProject(AddUserToProjectRequest request)
        {
            var user = _userService.GetUser(request.UserId).Data;

            if (!IsProjectExist(request.ProjectId).IsSuccess)
            {
                return new ErrorResult("Proje Bulunamadı.");
            }


            if (!IsProjectActive(request.ProjectId).IsSuccess)
            {
                return new ErrorResult("Aktif Olmayan Projeye Kullanıcı Ekleyemezsiniz.");
            }


            if (!IsUserProjectLeader(request.ProjectId, _authService.GetAuthenticatedUser().Data).IsSuccess)
            {
                return new ErrorResult("Yöneticisi Olmadığınız Projeye Kullanıcı Ekleyemezsiniz.");
            }


            if (user == null)
            {
                return new ErrorResult("Kullanıcı Bulunamadı.");
            }


            if (UserIsMemberOfProject(user, request.ProjectId).IsSuccess)
            {
                return new ErrorResult("Kullanıcı Aktif Olarak Projede Yer Alıyor.");
            }


            _projectRepository.AddUserToProject(user,request.ProjectId);
            _projectRepository.Save();

            _cacheManager.Remove($"project:{request.ProjectId}/members");

            return new SuccessResult("Kullanıcı Projeye Eklendi.");

        }






        public IBusinessResult RemoveUserFromProject(RemoveUserFromProjectRequest request)
        {
            var user = _userService.GetUser(request.UserId).Data;


            if (!IsProjectExist(request.ProjectId).IsSuccess)
            {
                return new ErrorResult("Proje Bulunamadı.");
            }


            if (!IsProjectActive(request.ProjectId).IsSuccess)
            {
                return new ErrorResult("Aktif Olmayan Projeye Kullanıcı Ekleyemezsiniz.");
            }


            if (!IsUserProjectLeader(request.ProjectId, _authService.GetAuthenticatedUser().Data).IsSuccess)
            {
                return new ErrorResult("Yöneticisi Olmadığınız Projeye Kullanıcı Ekleyemezsiniz.");
            }


            if (user == null)
            {
                return new ErrorResult("Kullanıcı Bulunamadı.");
            }


            if (!UserIsMemberOfProject(user, request.ProjectId).IsSuccess)
            {
                return new ErrorResult("Projeye Yer Almayan Bir Kullanıcı Projeden Çıkaramazsınız.");
            }


            _projectRepository.RemoveUserFromProject(request.UserId, request.ProjectId);
            _projectRepository.Save();

            _cacheManager.Remove($"project:{request.ProjectId}/members");


            return new SuccessResult("Kullanıcı Projeden Çıkarıldı.");
        }





        public IBusinessResult UserIsMemberOfProject(User user, int projectId)
        {
            var projectMembers = _projectRepository.Get(x => x.Id == projectId, x => x.Members).Members;

            if (projectMembers.FirstOrDefault(x => x.Id == user.Id) == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();;
        }




        public IBusinessDataResult<ICollection<UserListResponse>> GetUsersOutOfTheProject(int projectId)
        {
            return new SuccessDataResult<ICollection<UserListResponse>>(_userService.GetUsersOutOfTheProject(projectId).Data);
        }




    }
}