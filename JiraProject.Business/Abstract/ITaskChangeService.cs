using System.Collections.Generic;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.Entities.DataTransferObjects.Dto;
using JiraProject.Entities.DataTransferObjects.Response;
using JiraProject.Entities.Entities;

namespace JiraProject.Business.Abstract
{
    public interface ITaskChangeService
    {
        IBusinessDataResult<TaskChange> CreateTaskChange(CreateTaskChangeDto taskChange);
        IBusinessDataResult<ICollection<TaskChangeListResponse>> GetTaskChangesByTask(int taskId);
    }
}