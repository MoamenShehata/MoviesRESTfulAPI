using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MoviesRESTfulAPI.Repository
{
    public interface IRpository<TEntity, TId> where TEntity : class
    {
        TEntity Get(TId id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
