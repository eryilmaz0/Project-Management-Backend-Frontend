using JiraProject.Entities.Enums;

namespace JiraProject.Entities.DataTransferObjects.Request
{
    public class CreateTaskRequest
    {
        public string TaskDescription { get; set; }
        public int ProjectId { get; set; }
        public PriorityLevel Priority { get; set; }
        public TaskType Type { get; set; }
        public int AssignedUserId { get; set; }
    }
}