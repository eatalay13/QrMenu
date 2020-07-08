using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryBusiness _categoryBusiness;

        public CategoryController(ICategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }

        public IActionResult Index()
        {
            var categories = _categoryBusiness.GetCategoryList();

            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void AddCategory(Category category)
        {
            _categoryBusiness.AddCategory(category);
        }
    }
}
