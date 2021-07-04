using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using JiraProject.Business.Abstract;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.DataAccess.Abstract;
using JiraProject.Entities.DataTransferObjects.Dto;
using JiraProject.Entities.DataTransferObjects.Response;
using JiraProject.Entities.Entities;

namespace JiraProject.Business.Concrete
{
    public class TaskChangeManager : ITaskChangeService
    {

        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly ITaskChangeRepository _taskChangeRepository;
        private readonly ICacheService _cacheManager;
       


        public TaskChangeManager( IMapper mapper, IAuthService authService, ITaskChangeRepository taskChangeRepository, ICacheService cacheManager)
        {
            _mapper = mapper;
            _authService = authService;
            _taskChangeRepository = taskChangeRepository;
            _cacheManager = cacheManager;

        }



        public IBusinessDataResult<TaskChange> CreateTaskChange(CreateTaskChangeDto dto)
        {
            var newTaskChange = _mapper.Map<TaskChange>(dto);
            newTaskChange.UserId = _authService.GetAuthenticatedUser().Data.Id;
            return new SuccessDataResult<TaskChange>(newTaskChange);
        }




        public IBusinessDataResult<ICollection<TaskChangeListResponse>> GetTaskChangesByTask(int taskId)
        {

            if (_cacheManager.IsKeyExist($"task:{taskId}/changes"))
            {
                var taskChangesFromCache = _cacheManager.Get<ICollection<TaskChangeListResponse>>($"task:{taskId}/changes");
                return new SuccessDataResult<ICollection<TaskChangeListResponse>>(taskChangesFromCache);
            }

            var taskChanges = _taskChangeRepository.GetAll(x => x.TaskId == taskId, x => x.User);
            var mappedTaskChanges = _mapper.Map<ICollection<TaskChangeListResponse>>(taskChanges);

            _cacheManager.Add($"task:{taskId}/changes", mappedTaskChanges, 60);
            return new SuccessDataResult<ICollection<TaskChangeListResponse>>(mappedTaskChanges);
        }



        
    }
}