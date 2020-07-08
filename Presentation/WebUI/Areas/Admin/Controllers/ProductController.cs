using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Entities.Dtos.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductBusiness _productBusiness;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBusiness = productBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View(new AddProductDto());
        }

        [HttpPost]
        public IActionResult Add(AddProductDto addProduct)
        {
            _productBusiness.AddProduct(addProduct);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var product = _productBusiness.GetProductDetail(id);

            return View(product);
        }

        [HttpPost]
        public IActionResult Update(UpdateProductDto updateProduct)
        {
            _productBusiness.UpdateProduct(updateProduct);

            return RedirectToAction(nameof(Index));
        }
    }
}
