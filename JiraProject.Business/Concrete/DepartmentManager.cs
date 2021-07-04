using JiraProject.Business.Abstract;
using JiraProject.Business.BusinessResultObjects;
using JiraProject.DataAccess.Abstract;

namespace JiraProject.Business.Concrete
{
    public class DepartmentManager : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;


        public DepartmentManager(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
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
    }
}