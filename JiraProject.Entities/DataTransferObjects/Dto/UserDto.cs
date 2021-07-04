namespace JiraProject.Entities.DataTransferObjects.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }


        public UserDto(int id, string name, string lastName, string picture)
        {
            this.Id = id;
            this.Name = name;
            this.LastName = lastName;
            this.Picture = picture;
        }
    }
}