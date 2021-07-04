using System.Collections.Generic;

namespace JiraProject.Entities.Entities
{
    public class Project : EntityBase
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int DepartmentId { get; set; }
        public int ProjectLeaderId { get; set; }
        public bool IsActive { get; set; }


        //NAV PROPS
        
        public virtual Department Department { get; set; }
        public virtual List<Task> Tasks { get; set; }
        public virtual List<User> Members { get; set; }  //Many-Many
        public User ProjectLeader { get; set; }



        public Project():base()
        {
            this.Tasks = new List<Task>();
            this.Members = new List<User>();
            this.IsActive = true;
        }
    }
}