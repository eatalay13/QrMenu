using Core.DataAccess;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> Category { get; }
        IRepository<Product> Product { get; }

        void SaveChanges();
    }
}
