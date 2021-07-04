using System.Collections.Generic;

namespace JiraProject.Entities.Entities
{
    public class Department : EntityBase
    {
        public string DepartmentName { get; set; }
        public string Description { get; set; }


        //
        public List<Project> Projects { get; set; }


        public Department():base()
        {
            this.Projects = new List<Project>();
        }
    }
}