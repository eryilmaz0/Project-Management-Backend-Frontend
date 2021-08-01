namespace JiraProject.Entities.DataTransferObjects.Dto
{
    public class DepartmentListDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public int ProjectCount { get; set; }
    }
}