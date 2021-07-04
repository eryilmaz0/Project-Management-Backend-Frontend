using JiraProject.Business.BusinessResultObjects;

namespace JiraProject.Business.Abstract
{
    public interface IDepartmentService
    {
        IBusinessResult IsDepartmentExist(int departmentId);
    }
}