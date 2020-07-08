using Business.Interfaces;
using Data.Repository.Contexts;
using DevExtreme.AspNet.Mvc;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Globalization;
using System.Threading.Tasks;
using WebUI.Framework.Extensions;

namespace WebUI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProductsController : Controller
    {
        private readonly ECommerceContext _context;
        private readonly IProductBusiness _productBusiness;

        public ProductsController(ECommerceContext context, IProductBusiness productBusiness)
        {
            _context = context;
            _productBusiness = productBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var result = await _productBusiness.BindDevExp(loadOptions);

            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values)
        {
            var model = await _context.Product.FirstOrDefaultAsync(item => item.Id == key);
            if (model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(ModelState.GetFullErrorMessage());

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public void Delete(int key)
        {
            _productBusiness.DeleteProduct(key);
        }

        private void PopulateModel(Product model, IDictionary values)
        {
            string ID = nameof(Product.Id);
            string NAME = nameof(Product.Name);
            string IMAGE = nameof(Product.Image);
            string DESCRIPTION = nameof(Product.Description);
            string PRICE = nameof(Product.Price);

            if (values.Contains(ID))
            {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if (values.Contains(NAME))
            {
                model.Name = Convert.ToString(values[NAME]);
            }

            if (values.Contains(IMAGE))
            {
                model.Image = Convert.ToString(values[IMAGE]);
            }

            if (values.Contains(DESCRIPTION))
            {
                model.Description = Convert.ToString(values[DESCRIPTION]);
            }

            if (values.Contains(PRICE))
            {
                model.Price = Convert.ToDecimal(values[PRICE], CultureInfo.InvariantCulture);
            }
        }
    }
}