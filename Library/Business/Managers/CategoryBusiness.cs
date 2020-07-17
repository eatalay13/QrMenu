using Business.Interfaces;
using Core.Exceptions;
using Core.Utilities.TreeItem;
using Data.UnitOfWork;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Entities.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly IUnitOfWork _uow;

        public CategoryBusiness(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddCategory(Category category)
        {
            _uow.Category.Insert(category);
            _uow.SaveChanges();
        }

        public async Task<LoadResult> BindDevExp(DataSourceLoadOptions loadOptions)
        {
            var data = _uow.Category.TableNoTracking;

            return await DataSourceLoader.LoadAsync(data.OrderBy(o => o.SortOrder), loadOptions);
        }

        public void DeleteCategory(int id)
        {
            var category = GetByIdCategory(id);

            _uow.Category.Delete(category);
            _uow.SaveChanges();
        }

        public Category GetByIdCategory(int id)
        {
            var category = _uow.Category.Table.FirstOrDefault(e => e.Id == id);
            if (category == null)
                throw new BusinessException("Kayıtlı kategori bulunamadı!");

            return category;
        }

        public ICollection<Category> GetCategoryList()
        {
            return _uow.Category.TableNoTracking.ToList();
        }

        public IEnumerable<TreeItem<Category>> GetCategoryTreeList()
        {
            var categories = GetCategoryList().AsEnumerable();
            return categories.GenerateTree(c => c.Id, c => c.ParentCategoryId);
        }

        public void UpdateCategory(Category category)
        {
            _uow.Category.Update(category);
            _uow.SaveChanges();
        }
    }
}
