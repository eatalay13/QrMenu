using Business.Interfaces;
using DevExtreme.AspNet.Mvc;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Threading.Tasks;
using WebUI.Framework.Extensions;

namespace WebUI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CafeTablesController : Controller
    {
        private readonly ICafeTableBusiness _cafeTableBusiness;

        public CafeTablesController(ICafeTableBusiness cafeTableBusiness)
        {
            _cafeTableBusiness = cafeTableBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var result = await _cafeTableBusiness.BindDevExp(loadOptions);

            return Json(result);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new CafeTable();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(ModelState.GetFullErrorMessage());

            _cafeTableBusiness.AddCafeTable(model);

            return Json(model.Id);
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var model = _cafeTableBusiness.GetByIdCafeTable(key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(ModelState.GetFullErrorMessage());

            _cafeTableBusiness.UpdateCafeTable(model);
            return Ok();
        }

        [HttpDelete]
        public void Delete(int key)
        {
            _cafeTableBusiness.DeleteCafeTable(key);
        }

        private void PopulateModel(CafeTable model, IDictionary values)
        {
            string ID = nameof(CafeTable.Id);
            string NAME = nameof(CafeTable.Name);
            string NUMBER = nameof(CafeTable.Number);
            string IS_BUSY = nameof(CafeTable.IsBusy);

            if (values.Contains(ID))
            {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if (values.Contains(NAME))
            {
                model.Name = Convert.ToString(values[NAME]);
            }

            if (values.Contains(NUMBER))
            {
                model.Number = Convert.ToInt32(values[NUMBER]);
            }

            if (values.Contains(IS_BUSY))
            {
                model.IsBusy = Convert.ToBoolean(values[IS_BUSY]);
            }
        }
    }
}