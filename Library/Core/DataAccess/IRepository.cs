using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Core.DataAccess
{
    public interface IRepository<T> where T : class, new()
    {
        #region Methods
        T GetById(object id);
        void Insert(T entity);
        void Insert(IEnumerable<T> entities);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);

        #endregion

        #region Properties

        IQueryable<T> Table { get; }

        IQueryable<T> TableNoTracking { get; }

        DbSet<T> TableRaw { get; }

        #endregion
    }
}
