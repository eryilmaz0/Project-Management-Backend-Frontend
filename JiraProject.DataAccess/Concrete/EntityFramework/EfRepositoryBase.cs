using JiraProject.DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JiraProject.DataAccess.Extensions;
using JiraProject.Entities.DbContext;
using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraProject.DataAccess.Concrete.EntityFramework
{
    public class EfRepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase,new()
    {

        private readonly JiraDbContext _context;


        public EfRepositoryBase(JiraDbContext context)
        {
            _context = context;
        }



        public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().IncludeMultiple(includes).AsNoTracking();

            return predicate == null 
                    ? query.ToList() 
                    : query.Where(predicate).ToList();
        }



        public TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            return _context.Set<TEntity>().AsNoTracking().IncludeMultiple(includes).FirstOrDefault(predicate);
        }



        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }



        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }



        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }



        public void Delete(int entityId)
        {
            _context.Set<TEntity>().Remove(Get(x => x.Id == entityId));
        }



        public void Save()
        {
            _context.SaveChanges();
        }
    }
}