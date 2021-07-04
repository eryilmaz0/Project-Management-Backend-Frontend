namespace JiraProject.Entities.DataTransferObjects.Request
{
    public class CreateProjectRequest
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int DepartmentId { get; set; }
    }
}