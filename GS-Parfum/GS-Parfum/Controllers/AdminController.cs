using GS_Parfum.DAL.Repositories;
using GS_Parfum.Service.Implementations;
using GS_Parfum.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace GS_Parfum.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProductService _productService;
        private readonly UserRepository _userRepository;

        public AdminController(ProductService productService, UserRepository userRepository)
        {
            _productService = productService;
            _userRepository = userRepository;
        }

        [CustomAuthorize(GS_Parfum.Domain.Enum.Role.ROLE_ADMIN)]
        public async Task<ActionResult> AdminPanel()
        {
            var productsResponse = await _productService.Select();
            var productCount = productsResponse.Data.Count();
            ViewBag.ProductCount = productCount;

            var userResponse = await _userRepository.Select();
            var userCount = userResponse.Count();
            ViewBag.UserCount = userCount;

            return View();
        }
    }
}