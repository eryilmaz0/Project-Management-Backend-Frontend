using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JiraProject.Entities.DataTransferObjects.Dto;
using JiraProject.Entities.DataTransferObjects.Request;
using JiraProject.Entities.DataTransferObjects.Response;
using JiraProject.Entities.Entities;
using Task = JiraProject.Entities.Entities.Task;

namespace JiraProject.Business.MapperProfile.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<CreateProjectRequest, Project>();
            CreateMap<EditProjectRequest, Project>();
            CreateMap<CreateTaskRequest, Task>();
            CreateMap<User, UserListResponse>();

            //ENTITY --> DTO
            CreateMap<Project, ProjectListResponse>()
                .ForMember(x=>x.Department, opt => opt.MapFrom(x=> new DepartmentDto(x.Department.Id, x.Department.DepartmentName)))
                .ForMember(x=>x.ProjectLeader, opt => opt.MapFrom(x=> new UserDto(x.ProjectLeader.Id, x.ProjectLeader.Name, x.ProjectLeader.LastName, x.ProjectLeader.Picture)))
                .ForMember(x=>x.TaskCount, opt => opt.MapFrom(x=>x.Tasks.Count))
                .ForMember(x=>x.MemberCount, opt => opt.MapFrom(x=>x.Members.Count));


            CreateMap<Project, ProjectDetailResponse>()
                .ForMember(x=>x.Department, opt => opt.MapFrom(x=> new DepartmentDto(x.Department.Id, x.Department.DepartmentName)))
                .ForMember(x=>x.ProjectLeader, opt => opt.MapFrom(x=> new UserDto(x.ProjectLeader.Id, x.ProjectLeader.Name, x.ProjectLeader.LastName, x.ProjectLeader.Picture)))
                .ForMember(x=>x.MemberCount, opt => opt.MapFrom(x=>x.Members.Count));
                
                

            CreateMap<Task, TaskListResponse>()
                .ForMember(x=>x.AssignedUser, opt => opt.MapFrom(x=> new UserDto(x.User.Id, x.User.Name, x.User.LastName, x.User.Picture)))
                .ForMember(x=>x.TaskChangeCount, opt => opt.MapFrom(x=>x.TaskChanges.Count));



            CreateMap<CreateTaskRequest, CreateTaskChangeDto>();
            CreateMap<Task, CreateTaskChangeDto>();


            CreateMap<CreateTaskChangeDto, TaskChange>()
                .ForMember(x => x.TaskDescriptionValue, opt => opt.MapFrom(x => x.TaskDescription))
                .ForMember(x => x.PriorityValue, opt => opt.MapFrom(x => x.Priority))
                .ForMember(x => x.StatusValue, opt => opt.MapFrom(x => x.Status));


            CreateMap<EditTaskRequest, Task>();

            CreateMap<TaskChange, TaskChangeListResponse>()
                .ForMember(x=>x.User, opt => opt.MapFrom(x=> new UserDto(x.UserId, x.User.Name, x.User.LastName, x.User.Picture)));


            CreateMap<Department, DepartmentListDto>().ForMember(x=>x.ProjectCount, opt => opt.MapFrom(x=>x.Projects.Count));

        }
    }
}