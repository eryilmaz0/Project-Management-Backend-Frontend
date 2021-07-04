namespace JiraProject.Entities.DataTransferObjects.Request
{
    public class RemoveUserFromProjectRequest
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}