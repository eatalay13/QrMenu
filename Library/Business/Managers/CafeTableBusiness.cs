using Business.Interfaces;
using Core.Exceptions;
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
    public class CafeTableBusiness : ICafeTableBusiness
    {
        private readonly IUnitOfWork _uow;

        public CafeTableBusiness(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void AddCafeTable(CafeTable CafeTable)
        {
            _uow.CafeTable.Insert(CafeTable);
            _uow.SaveChanges();
        }

        public async Task<LoadResult> BindDevExp(DataSourceLoadOptions loadOptions)
        {
            var data = _uow.CafeTable.TableNoTracking;

            return await DataSourceLoader.LoadAsync(data, loadOptions);
        }

        public void DeleteCafeTable(int id)
        {
            var CafeTable = GetByIdCafeTable(id);

            _uow.CafeTable.Delete(CafeTable);
            _uow.SaveChanges();
        }

        public CafeTable GetByIdCafeTable(int id)
        {
            var CafeTable = _uow.CafeTable.Table.FirstOrDefault(e => e.Id == id);
            if (CafeTable == null)
                throw new BusinessException("Kayıtlı Masa bulunamadı!");

            return CafeTable;
        }

        public ICollection<CafeTable> GetCafeTableList()
        {
            return _uow.CafeTable.TableNoTracking.ToList();
        }

        public void UpdateCafeTable(CafeTable CafeTable)
        {
            _uow.CafeTable.Update(CafeTable);
            _uow.SaveChanges();
        }
    }
}
