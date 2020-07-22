using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult CldrData()
        {
            return new CldrDataScriptBuilder()
                .SetCldrPath("~/wwwroot/cldr-data")
                .UseLocales(new[] { "tr" })
                .Build();
        }
    }
}
