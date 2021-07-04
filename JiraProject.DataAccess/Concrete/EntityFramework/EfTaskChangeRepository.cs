using JiraProject.Entities.DbContext;
using JiraProject.Entities.Entities;

namespace JiraProject.DataAccess.Concrete.EntityFramework
{
    public class EfTaskChangeRepository : EfRepositoryBase<TaskChange>, Abstract.ITaskChangeRepository
    {

        private readonly JiraDbContext _context;


        public EfTaskChangeRepository(JiraDbContext context) : base(context)
        {
            _context = context;
        }
    }
}