using System.Collections.Generic;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.Entities.DataTransferObjects.Request;
using JiraProject.Entities.DataTransferObjects.Response;
using JiraProject.Entities.Entities;

namespace JiraProject.Business.Abstract
{
    public interface ITaskService
    {
        IBusinessDataResult<ICollection<TaskListResponse>> GetTasksByProject(int projectId);
        IBusinessResult CreateTask(CreateTaskRequest request);
        IBusinessResult EditTask(EditTaskRequest request);
        IBusinessResult IsTaskExist(int taskId);
        IBusinessDataResult<Task> GetTaskById(int taskId);
        IBusinessDataResult<List<TaskChangeListResponse>> GetTaskChangesByTask(int taskId);
    }
}