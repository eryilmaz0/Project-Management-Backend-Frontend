namespace JiraProject.Entities.DataTransferObjects.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }


        public DepartmentDto(int id, string departmentName)
        {
            this.Id = id;
            this.DepartmentName = departmentName;
        }
    }
}