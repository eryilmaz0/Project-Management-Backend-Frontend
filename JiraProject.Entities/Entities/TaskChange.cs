using JiraProject.Entities.Enums;

namespace JiraProject.Entities.Entities
{
    public class TaskChange : EntityBase
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string TaskDescriptionValue { get; set; }
        public PriorityLevel PriorityValue { get; set; }
        public TaskStatus StatusValue { get; set; }


        //
        public virtual Task Task { get; set; }
        public virtual User User { get; set; }


        public TaskChange():base()
        {
            
        }
    }
}