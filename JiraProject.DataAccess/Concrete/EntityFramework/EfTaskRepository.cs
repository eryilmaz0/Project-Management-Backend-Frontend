using System.Collections.Generic;
using System.Linq;
using JiraProject.DataAccess.Abstract;
using JiraProject.Entities.DbContext;
using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraProject.DataAccess.Concrete.EntityFramework
{
    public class EfTaskRepository : EfRepositoryBase<Task>, ITaskRepository
    {
        private readonly JiraDbContext _context;


        public EfTaskRepository(JiraDbContext context) : base(context)
        {
            _context = context;
        }

    }
}