using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataAccess
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
    where TEntity : class, new()
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            TableRaw = _context.Set<TEntity>();
        }

        ~RepositoryBase()
        {
            _context.Dispose();
        }

        public void Delete(TEntity entity)
        {
            TableRaw.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            TableRaw.RemoveRange(entities);
        }

        public TEntity GetById(object id)
        {
            return TableRaw.Find(id);
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            TableRaw.Add(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            TableRaw.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            TableRaw.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            TableRaw.UpdateRange(entities);
        }

        public IQueryable<TEntity> Table => TableRaw;
        public IQueryable<TEntity> TableNoTracking => TableRaw.AsNoTracking();

        public DbSet<TEntity> TableRaw { get; }
    }
}
