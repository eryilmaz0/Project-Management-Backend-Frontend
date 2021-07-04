using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using JiraProject.Entities.DbContext;
using JiraProject.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace JiraProject.DataAccess.Extensions
{
    public static class EntityFrameworkIncludeExtensions
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T,object>>[] includes) where T: EntityBase, new()
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
    }
}