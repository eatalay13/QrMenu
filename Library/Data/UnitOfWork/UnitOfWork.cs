using Core.DataAccess;
using Data.Repository.Contexts;
using Entities.Models;
using System;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceContext _dbContext;


        #region Lazy
        private readonly Lazy<IRepository<Category>> _category;
        private readonly Lazy<IRepository<Product>> _product;
        private readonly Lazy<IRepository<ProductToCategory>> _productToCategory;
        #endregion

        #region RepoInitiate
        public IRepository<Category> Category => _category.Value;

        public IRepository<Product> Product => _product.Value;

        public IRepository<ProductToCategory> ProductToCategory => _productToCategory.Value;


        #endregion

        #region Ctor
        public UnitOfWork(ECommerceContext context)
        {
            _dbContext = context;

            if (context == null)
            {
                throw new ArgumentNullException("Db Context Can Not Be Null");
            }

            _category = CreateRepo<Category>();
            _product = CreateRepo<Product>();
            _productToCategory = CreateRepo<ProductToCategory>();
        }

        #endregion

        #region Methods
        public void SaveChanges()
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                _dbContext.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private Lazy<IRepository<TModel>> CreateRepo<TModel>() where TModel : class, new()
        {
            return new Lazy<IRepository<TModel>>(() => new RepositoryBase<TModel>(_dbContext));
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
