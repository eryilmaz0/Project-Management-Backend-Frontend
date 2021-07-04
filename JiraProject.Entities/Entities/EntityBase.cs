using System;

namespace JiraProject.Entities.Entities
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }




        public EntityBase()
        {
            this.Created = this.LastUpdated = DateTime.Now;
        }
    }
}