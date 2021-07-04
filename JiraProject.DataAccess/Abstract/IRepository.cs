using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JiraProject.Entities.Entities;

namespace JiraProject.DataAccess.Abstract
{
    public interface IRepository<TEntity> where TEntity : EntityBase, new()
    {
        ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity,object>>[] includes);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int entityId);
        void Save();
    }
}