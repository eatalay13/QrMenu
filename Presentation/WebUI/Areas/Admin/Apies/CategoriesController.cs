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
    public class CategoriesController : Controller
    {
        private readonly ICategoryBusiness _categoryBusiness;

        public CategoriesController(ICategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var result = await _categoryBusiness.BindDevExp(loadOptions);

            return Json(result);
        }

        [HttpPost]
        public IActionResult Post(string values)
        {
            var model = new Category();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(ModelState.GetFullErrorMessage());

            _categoryBusiness.AddCategory(model);

            return Json(model.Id);
        }

        [HttpPut]
        public IActionResult Put(int key, string values)
        {
            var model = _categoryBusiness.GetByIdCategory(key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(ModelState.GetFullErrorMessage());

            _categoryBusiness.UpdateCategory(model);
            return Ok();
        }

        [HttpDelete]
        public void Delete(int key)
        {
            _categoryBusiness.DeleteCategory(key);
        }

        private void PopulateModel(Category model, IDictionary values)
        {
            string ID = nameof(Category.Id);
            string NAME = nameof(Category.Name);
            string PARENT_CATEGORY_ID = nameof(Category.ParentCategoryId);
            string SORT_ORDER = nameof(Category.SortOrder);
            string DESCRIPTION = nameof(Category.Description);

            if (values.Contains(ID))
            {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if (values.Contains(NAME))
            {
                model.Name = Convert.ToString(values[NAME]);
            }

            if (values.Contains(PARENT_CATEGORY_ID))
            {
                model.ParentCategoryId = values[PARENT_CATEGORY_ID] != null ? Convert.ToInt32(values[PARENT_CATEGORY_ID]) : (int?)null;
            }

            if (values.Contains(SORT_ORDER))
            {
                model.SortOrder = Convert.ToInt32(values[SORT_ORDER]);
            }

            if (values.Contains(DESCRIPTION))
            {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }
        }
    }
}