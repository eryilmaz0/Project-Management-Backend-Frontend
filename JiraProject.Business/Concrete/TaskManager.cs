using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using JiraProject.Business.Abstract;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.DataAccess.Abstract;
using JiraProject.Entities.DataTransferObjects.Dto;
using JiraProject.Entities.DataTransferObjects.Request;
using JiraProject.Entities.DataTransferObjects.Response;
using JiraProject.Entities.Entities;

namespace JiraProject.Business.Concrete
{
    public class TaskManager : ITaskService
    {
        private readonly IMapper _mapper;
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectService _projectService;
        private readonly ITaskChangeService _taskChangeService;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly ICacheService _cacheService;



        public TaskManager(IMapper mapper, ITaskRepository taskRepository, IProjectService projectService, ICacheService cacheService,
                           ITaskChangeService taskChangeService, IAuthService authService, IUserService userService)
        {
            _mapper = mapper;
            _taskRepository = taskRepository;
            _projectService = projectService;
            _taskChangeService = taskChangeService;
            _authService = authService;
            _userService = userService;
            _cacheService = cacheService;
        }



        public IBusinessDataResult<ICollection<TaskListResponse>> GetTasksByProject(int projectId)
        {
            if (!_projectService.IsProjectExist(projectId).IsSuccess)
            {
                return new ErrorDataResult<ICollection<TaskListResponse>>("Proje Bulunamadı.");
            }

            
            var tasksByProject = _taskRepository.GetAll(x=>x.ProjectId == projectId, x=>x.User, x=>x.TaskChanges).ToList();
            var mappedTasks = _mapper.Map<List<TaskListResponse>>(tasksByProject);

            return new SuccessDataResult<ICollection<TaskListResponse>>(mappedTasks);

        }




        public IBusinessDataResult<Task> GetTaskById(int taskId)
        {
            var task = _taskRepository.Get(x => x.Id == taskId);

            if (task == null)
            {
                return new ErrorDataResult<Task>("Görev Bulunamadı.");
            }

            return new SuccessDataResult<Task>(task);
        }





        public IBusinessDataResult<List<TaskChangeListResponse>> GetTaskChangesByTask(int taskId)
        {
            var task = _taskRepository.Get(x => x.Id == taskId);
            var authenticatedUser = _authService.GetAuthenticatedUser().Data;

            if (task == null)
            {
                return new ErrorDataResult<List<TaskChangeListResponse>>("Görev Bulunamadı.");
            }


            if (!_projectService.UserIsMemberOfProject(authenticatedUser, task.ProjectId).IsSuccess 
                && !_projectService.IsUserProjectLeader(task.ProjectId, authenticatedUser).IsSuccess)
            {
                return new ErrorDataResult<List<TaskChangeListResponse>>("Üyesi Olmadığınız Proje Bilgilerini Göremezsiniz.");
            }


            var taskChanges = _taskChangeService.GetTaskChangesByTask(taskId).Data.ToList();
            var mappedTaskChanges = _mapper.Map<List<TaskChangeListResponse>>(taskChanges);

            return new SuccessDataResult<List<TaskChangeListResponse>>(mappedTaskChanges);


        }




        public IBusinessResult CreateTask(CreateTaskRequest request)
        {
            if (!_projectService.IsProjectExist(request.ProjectId).IsSuccess)
            {
                return new ErrorResult("Proje Bulunamadı.");
            }


            if (!_projectService.IsProjectActive(request.ProjectId).IsSuccess)
            {
                return new ErrorResult("Proje Aktif Değil.");
            }


            if (!_userService.IsUserExist(request.AssignedUserId).IsSuccess)
            {
                return new ErrorResult("Kullanıcı Bulunamadı.");
            }

            if (!_projectService.IsUserProjectLeader(request.ProjectId, _authService.GetAuthenticatedUser().Data).IsSuccess)
            {
                return new ErrorResult("Yöneticisi Olmadığınız Projeye Görev Ekleyemezsiniz.");
            }


            var newTask = _mapper.Map<Task>(request);
            var newTaskChange = _taskChangeService.CreateTaskChange(_mapper.Map<CreateTaskChangeDto>(request)).Data;
            newTask.TaskChanges.Add(newTaskChange);
            _taskRepository.Add(newTask);
            _taskRepository.Save();

            return new SuccessResult("Görev Başarıyla Oluşturuldu.");
            
        }




        public IBusinessResult EditTask(EditTaskRequest request)
        {
            //Sadece projenin yöneticisi veya atanmış yazılımcı güncelleyebilir

            var task = _taskRepository.Get(x => x.Id == request.Id, x=>x.Project);
            var authenticatedUser = _authService.GetAuthenticatedUser().Data;

            if (task == null)
            {
                return new ErrorResult("Görev Bulunamadı.");
            }


            if (!_projectService.IsProjectActive(task.ProjectId).IsSuccess)
            {
                return new ErrorResult("Aktif Olmayan Projedeki Görevleri Düzenleyemezsiniz.");
            }


            if (authenticatedUser.Id != task.Project.ProjectLeaderId && authenticatedUser.Id != task.AssignedUserId)
            {
                return new ErrorResult("Bu Görevi Düzenleme Yetkiniz Yok.");
            }


            if (!_userService.IsUserExist(request.AssignedUserId).IsSuccess)
            {
                return new ErrorResult("Atanacak Kullanıcı Bulunamadı.");
            }


            if (!_projectService.UserIsMemberOfProject(_userService.GetUser(request.AssignedUserId).Data, task.ProjectId).IsSuccess)
            {
                return new ErrorResult("Projede Yer Almayan Kullanıcıya Görev Atayamazsınız.");
            }


            task.AssignedUserId = request.AssignedUserId;
            task.Priority = request.Priority;
            task.Status = request.Status;
            task.TaskDescription = request.TaskDescription;

            var newTaskChange = _taskChangeService.CreateTaskChange(_mapper.Map<CreateTaskChangeDto>(task)).Data;
            task.TaskChanges.Add(newTaskChange);
            
            _taskRepository.Update(task);
            _taskRepository.Save();


            //Cache'i Temizle
            if (_cacheService.IsKeyExist($"task:{task.Id}/changes"))
            {
                _cacheService.Remove($"task:{task.Id}/changes");
            }


            return new SuccessResult("Görev Başarıyla Düzenlendi.");
        }




        public IBusinessResult IsTaskExist(int taskId)
        {
            var task = _taskRepository.Get(x => x.Id == taskId);

            if (task == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }



    }
}