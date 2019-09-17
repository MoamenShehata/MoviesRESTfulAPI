using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MoviesRESTfulAPI.Repository
{
    public class Rpository<TEntity, TId> : IRpository<TEntity, TId>
        where TEntity : class
    {
        protected readonly DbContext context;
        public DbSet<TEntity> Entities => context.Set<TEntity>();
        public Rpository(DbContext _context)
        {
            context = _context;
        }

        public TEntity Get(TId id)
        {
            return context.Set<TEntity>().Find(id);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public void Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);

        }
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
