using JiraProject.DataAccess.Abstract;
using JiraProject.Entities.DbContext;
using JiraProject.Entities.Entities;

namespace JiraProject.DataAccess.Concrete.EntityFramework
{
    public class EfDepartmentRepository : EfRepositoryBase<Department>, IDepartmentRepository
    {

        private readonly JiraDbContext _context;


        public EfDepartmentRepository(JiraDbContext context):base(context)
        {
            _context = context;
        }
    }
}