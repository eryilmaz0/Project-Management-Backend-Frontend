using JiraProject.Entities.Enums;

namespace JiraProject.Entities.DataTransferObjects.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public PriorityLevel Priority { get; set; }
        public TaskType Type { get; set; }
        public TaskStatus Status { get; set; }

        public virtual UserDto AssignedUser { get; set; }

    }
}