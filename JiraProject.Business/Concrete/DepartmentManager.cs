using System.Collections.Generic;
using AutoMapper;
using JiraProject.Business.Abstract;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.DataAccess.Abstract;
using JiraProject.Entities.DataTransferObjects.Dto;

namespace JiraProject.Business.Concrete
{
    public class DepartmentManager : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;


        public DepartmentManager(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }



        public IBusinessResult IsDepartmentExist(int departmentId)
        {
            var department = _departmentRepository.Get(x => x.Id == departmentId);

            if (department == null)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }




        public IBusinessDataResult<ICollection<DepartmentListDto>> GetDepartments()
        {
            var departments = _departmentRepository.GetAll(includes: x => x.Projects);
            var mappedDepartments = _mapper.Map<List<DepartmentListDto>>(departments);

            return new SuccessDataResult<ICollection<DepartmentListDto>>(mappedDepartments);
        }
    }
}