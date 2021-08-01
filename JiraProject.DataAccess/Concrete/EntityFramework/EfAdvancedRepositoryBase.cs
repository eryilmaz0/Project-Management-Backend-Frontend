using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JiraProject.Entities.DbContext;
using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace JiraProject.DataAccess.Concrete.EntityFramework
{
    public class EfAdvancedRepositoryBase<TEntity> where TEntity: EntityBase
    {
        private readonly JiraDbContext _context;


        public EfAdvancedRepositoryBase(JiraDbContext context)
        {
            _context = context;
        }


        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate = null,
                                         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null,
                                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                         bool disableTracking = true)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }


            if (include != null)
            {
                query = include.Invoke(query);
            }


            if (orderby != null)
            {
                query = orderby.Invoke(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query.ToList();
        }






        public TEntity Get(Expression<Func<TEntity, bool>> predicate,
                           Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                           bool disableTracking = true)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }


            if (include != null)
            {
                query = include.Invoke(query);
            }


            return query.FirstOrDefault(predicate);
        }





        public PagedResult<TEntity> FindPaged(int pageSize, int pageNumber,
                                              Expression<Func<TEntity, bool>> predicate = null,
                                              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
                                              Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null,
                                              bool disableTracking = true)


        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }


            if (include != null)
            {
                query = include.Invoke(query);
            }

            if (orderby != null)
            {
                query = orderby.Invoke(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return new PagedResult<TEntity>()
            {
                Result = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
                ResultCount = query.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}