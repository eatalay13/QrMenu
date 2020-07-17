using Core.Utilities.TreeItem;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ICategoryBusiness
    {
        ICollection<Category> GetCategoryList();
        void AddCategory(Category category);
        Category GetByIdCategory(int id);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
        IEnumerable<TreeItem<Category>> GetCategoryTreeList();
        Task<LoadResult> BindDevExp(DataSourceLoadOptions loadOptions);
    }
}
