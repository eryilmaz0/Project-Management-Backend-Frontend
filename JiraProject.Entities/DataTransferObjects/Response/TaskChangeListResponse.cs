using System;
using JiraProject.Entities.DataTransferObjects.Dto;
using JiraProject.Entities.Enums;

namespace JiraProject.Entities.DataTransferObjects.Response
{
    public class TaskChangeListResponse
    {
        public string TaskDescriptionValue { get; set; }
        public PriorityLevel PriorityValue { get; set; }
        public TaskStatus StatusValue { get; set; }
        public DateTime Created { get; set; }

        public virtual UserDto User { get; set; }
    }
}