using System;
using JiraProject.Entities.DataTransferObjects.Dto;

namespace JiraProject.Entities.DataTransferObjects.Response
{
    public class ProjectListResponse
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
        public int TaskCount { get; set; }
        public int MemberCount { get; set; }


        public DepartmentDto Department { get; set; }
        public UserDto ProjectLeader { get; set; }

    }
}