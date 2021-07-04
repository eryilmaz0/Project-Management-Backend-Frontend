using JiraProject.Entities.Enums;

namespace JiraProject.Entities.DataTransferObjects.Dto
{
    public class CreateTaskChangeDto
    {
        public string TaskDescription { get; set; }
        public int CreatedUserId { get; set; }
        public PriorityLevel Priority { get; set; }
        public TaskStatus Status { get; set; }


        
        
    }
}