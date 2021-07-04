using System.Collections.Generic;
using System.Linq;
using JiraProject.DataAccess.Abstract;
using JiraProject.Entities.DbContext;
using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraProject.DataAccess.Concrete.EntityFramework
{
    public class EfRoleRepository : EfRepositoryBase<Role>, IRoleRepository
    {

        private readonly JiraDbContext _context;


        public EfRoleRepository(JiraDbContext context):base(context)
        {
            _context = context;
        }




        public IList<Role> GetUserRoles(User user)
        {
            var findUser = _context.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == user.Id);
            return findUser.Roles.ToList();
        }



        public bool IsUserInRole(User user, string role)
        {
            var findUser = _context.Users.Include(x => x.Roles).FirstOrDefault(x => x.Id == user.Id);
            var userRole = findUser.Roles.FirstOrDefault(x => x.RoleName.ToLower() == role.ToLower());

            return userRole == null ? false : true;
        }
    }
}