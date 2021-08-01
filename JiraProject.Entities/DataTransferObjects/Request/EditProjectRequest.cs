using System;

namespace JiraProject.Entities.DataTransferObjects.Request
{
    public class EditProjectRequest
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int DepartmentId { get; set; }
        public int ProjectLeaderId { get; set; }
    }
}