using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Entities.Dtos.Product;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IProductBusiness
    {
        ICollection<Product> GetProductList();
        void AddProduct(AddProductDto addProduct);
        UpdateProductDto GetProductDetail(int id);
        void UpdateProduct(UpdateProductDto updateProduct);
        void DeleteProduct(int id);
        Task<LoadResult> BindDevExp(DataSourceLoadOptions loadOptions);
    }
}
