using System.Collections.Generic;
using System.Linq;
using System.Xml;
using JiraProject.DataAccess.Abstract;
using JiraProject.Entities.DbContext;
using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraProject.DataAccess.Concrete.EntityFramework
{
    public class EfProjectRepository : EfRepositoryBase<Project>, IProjectRepository
    {

        private readonly JiraDbContext _context;


        public EfProjectRepository(JiraDbContext context) : base(context)
        {
            _context = context;
        }




        public void RemoveUserFromProject(int userId, int projectId)
        {
            var project = _context.Projects.Find(projectId);
            _context.Entry(project).Collection("Members").Load();
            project.Members.Remove(project.Members.FirstOrDefault(x => x.Id == userId));

        }



        public void AddUserToProject(User user, int projectId)
        {
            var project = _context.Projects.Find(projectId);
            _context.Entry(project).Collection("Members").Load();
            project.Members.Add(user);
        }
    }
}