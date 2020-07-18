using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ICafeTableBusiness
    {
        ICollection<CafeTable> GetCafeTableList();
        void AddCafeTable(CafeTable cafeTable);
        CafeTable GetByIdCafeTable(int id);
        void UpdateCafeTable(CafeTable cafeTable);
        void DeleteCafeTable(int id);
        Task<LoadResult> BindDevExp(DataSourceLoadOptions loadOptions);
    }
}
