using System.Collections.Generic;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.Entities.DataTransferObjects.Dto;

namespace JiraProject.Business.Abstract
{
    public interface IDepartmentService
    {
        IBusinessResult IsDepartmentExist(int departmentId);
        IBusinessDataResult<ICollection<DepartmentListDto>> GetDepartments();
    }
}