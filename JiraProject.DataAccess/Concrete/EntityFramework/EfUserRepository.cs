using JiraProject.DataAccess.Abstract;
using JiraProject.Entities.DbContext;
using JiraProject.Entities.Entities;

namespace JiraProject.DataAccess.Concrete.EntityFramework
{
    public class EfUserRepository : EfRepositoryBase<User>, IUserRepository
    {

        private readonly JiraDbContext _context;


        public EfUserRepository(JiraDbContext context):base(context)
        {
            _context = context;
        }
    }
}