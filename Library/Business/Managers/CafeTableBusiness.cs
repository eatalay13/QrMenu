using Business.Interfaces;
using Core.Exceptions;
using Core.QRCode;
using Core.ZipManager;
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
        private readonly QRCodeManager _qRCodeManager;
        private readonly ZipManager _zipManager;

        public CafeTableBusiness(IUnitOfWork uow, QRCodeManager qRCodeManager, ZipManager zipManager)
        {
            _uow = uow;
            _qRCodeManager = qRCodeManager;
            _zipManager = zipManager;
        }

        public void AddCafeTable(CafeTable CafeTable)
        {
            _uow.CafeTable.Insert(CafeTable);
            _uow.SaveChanges();
        }

        public byte[] GetTableQRCode(int tableId)
        {
            return _qRCodeManager.GenerateQrCode("https://localhost:44323?tableId=" + tableId);
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

        public byte[] GetAllQrCodeZip()
        {
            var tables = GetCafeTableList();
            List<string> images = new List<string>();
            foreach (var table in tables)
            {
                var tableImage = GetTableQRCode(table.Id);
                string imageName = _zipManager.ByteArrayToFile(table.Name, tableImage);
                images.Add(imageName);
            }

            return _zipManager.AddFiles("qrImages", images);
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
