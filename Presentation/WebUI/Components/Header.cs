using Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ICategoryBusiness _categoryBusiness;

        public HeaderViewComponent(ICategoryBusiness categoryBusiness)
        {
            _categoryBusiness = categoryBusiness;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _categoryBusiness.GetCategoryTreeList();
            return View(categories);
        }
    }
}
