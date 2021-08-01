using System;
using System.Collections.Generic;
using JiraProject.Entities.DataTransferObjects.Dto;

namespace JiraProject.Entities.DataTransferObjects.Response
{
    public class ProjectDetailResponse
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public int MemberCount { get; set; }
        public bool IsActive { get; set; }
   

      
        public DepartmentDto Department { get; set; }
        public UserDto ProjectLeader { get; set; }
     
    }
}