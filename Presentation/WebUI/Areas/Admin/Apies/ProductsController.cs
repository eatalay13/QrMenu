using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository.Contexts;
using Entities.Models;

namespace WebUI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private ECommerceContext _context;

        public ProductsController(ECommerceContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var product = _context.Product.Select(i => new {
                i.Id,
                i.StockCode,
                i.Name,
                i.Model,
                i.Quantity,
                i.Image,
                i.TaxClassId,
                i.Description,
                i.Price
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(product, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Product();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Product.Add(model);
            await _context.SaveChangesAsync();

            return Json(result.Entity.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Product.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Product.FirstOrDefaultAsync(item => item.Id == key);

            _context.Product.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> TaxClassLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.TaxClass
                         orderby i.Name
                         select new {
                             Value = i.Id,
                             Text = i.Name
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(Product model, IDictionary values) {
            string ID = nameof(Product.Id);
            string STOCK_CODE = nameof(Product.StockCode);
            string NAME = nameof(Product.Name);
            string MODEL = nameof(Product.Model);
            string QUANTITY = nameof(Product.Quantity);
            string IMAGE = nameof(Product.Image);
            string TAX_CLASS_ID = nameof(Product.TaxClassId);
            string DESCRIPTION = nameof(Product.Description);
            string PRICE = nameof(Product.Price);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(STOCK_CODE)) {
                model.StockCode = Convert.ToString(values[STOCK_CODE]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(MODEL)) {
                model.Model = Convert.ToString(values[MODEL]);
            }

            if(values.Contains(QUANTITY)) {
                model.Quantity = Convert.ToInt32(values[QUANTITY]);
            }

            if(values.Contains(IMAGE)) {
                model.Image = Convert.ToString(values[IMAGE]);
            }

            if(values.Contains(TAX_CLASS_ID)) {
                model.TaxClassId = values[TAX_CLASS_ID] != null ? Convert.ToInt32(values[TAX_CLASS_ID]) : (int?)null;
            }

            if(values.Contains(DESCRIPTION)) {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if(values.Contains(PRICE)) {
                model.Price = Convert.ToDecimal(values[PRICE], CultureInfo.InvariantCulture);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}