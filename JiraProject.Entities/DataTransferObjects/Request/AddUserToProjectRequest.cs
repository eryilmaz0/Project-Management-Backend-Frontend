namespace JiraProject.Entities.DataTransferObjects.Request
{
    public class AddUserToProjectRequest
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}