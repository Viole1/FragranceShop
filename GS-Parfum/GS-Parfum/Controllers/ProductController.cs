using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GS_Parfum.Domain.Entity;
using GS_Parfum.Domain.Entity.Product;
using GS_Parfum.Domain.Enum;
using GS_Parfum.Service.Interfaces;
using GS_Parfum.Util;
using static System.Net.Mime.MediaTypeNames;

namespace GS_Parfum.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [CustomAuthorize(Role.ROLE_USER)]

        public async Task<ActionResult> GetProducts()
        {
            var response = await _productService.Select(); // возвращает baseResponse
            return View(response.Data);
        }

        [HttpGet]
        // [CustomAuthorize(Role.ROLE_ADMIN)]
        public async Task<ActionResult> GetProduct(int id)
        {
            var response = await _productService.GetProduct(id);
            if (response.ResponseStatus == GS_Parfum.Domain.Enum.ResponseStatus.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.ResponseStatus == GS_Parfum.Domain.Enum.ResponseStatus.OK)
                return RedirectToAction("GetProducts");
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<ActionResult> Save(int id = 0)
        {
            if (id == 0)
            {
                var product = new Product();
                product.VolumePrices = new List<ProductVolumePrice>();
                product.Chords = new List<Chord>();
                product.TopNotes = new List<Note>();
                product.MiddleNotes = new List<Note>();
                product.BaseNotes = new List<Note>();
                return View(product);
            }
            var response = await _productService.GetProduct(id);
            if (response.ResponseStatus == GS_Parfum.Domain.Enum.ResponseStatus.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<ActionResult> Save(Product model, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (photo != null && photo.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(photo.InputStream))
                    {
                        model.Photo = binaryReader.ReadBytes(photo.ContentLength);
                    }
                }
                if (model.Id == 0)
                {
                    await _productService.CreateProduct(model);
                }
                else
                {
                    await _productService.Edit(model.Id, model);
                }
            }
            return RedirectToAction("GetProducts");
        }
    }
}