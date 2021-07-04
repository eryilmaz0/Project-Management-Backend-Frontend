using System.Collections.Generic;
using JiraProject.Entities.Enums;

namespace JiraProject.Entities.Entities
{
    public class User : EntityBase
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Picture { get; set; }


        //NAV PROPS
        public virtual List<Role> Roles { get; set; }       //Many-Many
        public virtual List<Project> Projects { get; set; }  //Many-Many
        public List<Project> AssignedProjects { get; set; }  //Leader's projects
        public virtual List<Task> Tasks { get; set; }
        public virtual List<TaskChange> TaskChanges { get; set; }



        public User():base()
        {
            this.Roles = new List<Role>();
            this.Projects = new List<Project>();
            this.Tasks = new List<Task>();
            this.TaskChanges = new List<TaskChange>();
            this.AssignedProjects = new List<Project>();
        }
    }
}