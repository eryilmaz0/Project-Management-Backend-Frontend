using JiraProject.Entities.Enums;

namespace JiraProject.Entities.DataTransferObjects.Request
{
    public class EditTaskRequest
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; }
        public PriorityLevel Priority { get; set; }
        public TaskStatus Status { get; set; }
        public int AssignedUserId { get; set; }
    }
}