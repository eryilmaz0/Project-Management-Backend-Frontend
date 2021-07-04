using JiraProject.Entities.Enums;

namespace JiraProject.Entities.DataTransferObjects.Request
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}