using System.Collections.Generic;
using JiraProject.Entities.Enums;

namespace JiraProject.Entities.Entities
{
    public class Task : EntityBase
    {
        public string TaskDescription { get; set; }
        public int ProjectId { get; set; }
        public PriorityLevel Priority { get; set; }
        public TaskType Type { get; set; }
        public TaskStatus Status { get; set; }
        public int AssignedUserId { get; set; } 

        //NAV PROPS
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
        public virtual List<TaskChange> TaskChanges { get; set; }

        public Task():base()
        {
            this.TaskChanges = new List<TaskChange>();
            this.Status = TaskStatus.ToDo;
        }
    }
}