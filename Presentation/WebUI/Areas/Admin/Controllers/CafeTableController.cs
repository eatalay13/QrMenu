using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CafeTableController : Controller
    {
        private readonly ICafeTableBusiness _cafeTableBusiness;

        public CafeTableController(ICafeTableBusiness cafeTableBusiness)
        {
            _cafeTableBusiness = cafeTableBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public Task GetQrCode(int id)
        {
            var imageByte = _cafeTableBusiness.GetTableQRCode(id);

            HttpContext.Response.Clear();
            HttpContext.Response.ContentType = "image/jpg";
            HttpContext.Response.Body.WriteAsync(imageByte, 0, imageByte.Length);

            return HttpContext.Response.WriteAsync("");
        }


        [HttpGet]
        public FileResult GetAllQrCode()
        {
            var content = _cafeTableBusiness.GetAllQrCodeZip();
            var contentType = "APPLICATION/octet-stream";
            var fileName = "qrCodes.zip";

            return File(content, contentType, fileName);
        }
    }
}
