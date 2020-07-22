using Business.Interfaces;
using Core.Exceptions;
using Data.UnitOfWork;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Entities.Dtos.Product;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public UpdateProductDto GetProductDetail(int id)
        {
            var product = _uow.Product.TableNoTracking
                .Include(e => e.ProductImage)
                .Select(e => new UpdateProductDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Photos = e.ProductImage.ToList(),
                    MainPhoto = e.Image,
                    Description = e.Description,
                    IsActive = e.IsActive,
                    Price = e.Price
                })
                .FirstOrDefault(e => e.Id == id);

            if (product == null)
                throw new BusinessException("Ürün bulunamadı.");

            product.CategoryIds = _uow.ProductToCategory.Table
                .Where(c => c.ProductId == id).Select(e => e.CategoryId).ToList();

            return product;
        }

        public void AddProduct(AddProductDto addProduct)
        {
            var product = new Product
            {
                Name = addProduct.Name,
                Description = addProduct.Description,
                Image = addProduct.MainPhoto,
                ProductImage = addProduct.Photos,
                IsActive = addProduct.IsActive,
                Price = addProduct.Price
            };

            _uow.Product.Insert(product);

            if (addProduct.CategoryIds == null || !addProduct.CategoryIds.Any())
                throw new BusinessException("Lütfen önce kategori seçiniz.");

            var productCategories = _uow.Category.Table
                .Where(c => addProduct.CategoryIds.Contains(c.Id))
                .Select(e => new ProductToCategory
                {
                    CategoryId = e.Id,
                    Product = product
                }).ToList();

            _uow.ProductToCategory.Insert(productCategories);

            _uow.SaveChanges();
        }

        public void UpdateProduct(UpdateProductDto updateProduct)
        {
            var product = _uow.Product.Table
                .Include(e => e.ProductImage)
                .FirstOrDefault(e => e.Id == updateProduct.Id);

            if (product == null)
                throw new BusinessException("Ürün bulunamadı.");

            product.Name = updateProduct.Name;
            product.Description = updateProduct.Description;
            product.Image = updateProduct.MainPhoto;
            product.ProductImage = updateProduct.Photos;
            product.IsActive = updateProduct.IsActive;
            product.Price = updateProduct.Price;

            _uow.Product.Update(product);

            var productCategories = _uow.ProductToCategory.Table
                .Where(c => c.ProductId == updateProduct.Id);

            foreach (var pCat in productCategories)
            {
                if (!updateProduct.CategoryIds.Contains(pCat.CategoryId))
                    _uow.ProductToCategory.Delete(pCat);
            }

            foreach (var pCatNew in updateProduct.CategoryIds)
            {
                if (!productCategories.Any(e => e.CategoryId == pCatNew && e.ProductId == updateProduct.Id))
                    _uow.ProductToCategory.Insert(new ProductToCategory
                    {
                        ProductId = updateProduct.Id,
                        CategoryId = pCatNew
                    });
            }

            _uow.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _uow.Product.Table
                .FirstOrDefault(e => e.Id == id);

            if (product == null)
                throw new BusinessException("Ürün bulunamadı.");

            _uow.Product.Delete(product);

            _uow.SaveChanges();
        }

        public async Task<LoadResult> BindDevExp(DataSourceLoadOptions loadOptions)
        {
            var data = _uow.Product.TableNoTracking;

            return await DataSourceLoader.LoadAsync(data, loadOptions);
        }
    }
}
