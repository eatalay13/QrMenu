using Entities.Dtos.Product;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IProductBusiness
    {
        ICollection<Product> GetProductList();
        void AddProduct(AddProductDto addProduct);
    }
}
