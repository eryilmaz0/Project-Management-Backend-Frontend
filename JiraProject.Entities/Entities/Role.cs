using System.Collections.Generic;

namespace JiraProject.Entities.Entities
{
    public class Role : EntityBase
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }


        public virtual List<User> Users { get; set; }


        public Role() : base()
        {
            this.Users = new List<User>();
        }

    }
}