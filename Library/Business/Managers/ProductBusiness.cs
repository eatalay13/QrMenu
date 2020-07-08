using Business.Interfaces;
using Data.UnitOfWork;
using Entities.Dtos.Product;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Managers
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IUnitOfWork _uow;

        public ProductBusiness(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ICollection<Product> GetProductList()
        {
            return _uow.Product.TableNoTracking.ToList();
        }

        public void AddProduct(AddProductDto addProduct)
        {
            
        }
    }
}
