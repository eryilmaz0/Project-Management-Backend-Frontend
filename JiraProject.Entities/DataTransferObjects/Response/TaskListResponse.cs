using System;
using JiraProject.Entities.DataTransferObjects.Dto;
using JiraProject.Entities.Enums;

namespace JiraProject.Entities.DataTransferObjects.Response
{
    public class TaskListResponse
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public PriorityLevel Priority { get; set; }
        public TaskType Type { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public int TaskChangeCount { get; set; }

        public UserDto AssignedUser { get; set; }
    }
}