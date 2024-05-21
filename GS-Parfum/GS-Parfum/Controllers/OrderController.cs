using GS_Parfum.Domain.Entity.Order;
using GS_Parfum.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GS_Parfum.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public OrderController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            var token = HttpContext.Request.Cookies["AuthToken"].Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var idString = jwtToken.Claims.ToList()[0].Value;
            int.TryParse(idString, out int id);
            order.UserId = id;

            await _orderService.CreateOrder(order);
            await _cartService.ClearCart(order.CartId);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<JsonResult> OrderList()
        {
            var token = HttpContext.Request.Cookies["AuthToken"].Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var idString = jwtToken.Claims.ToList()[0].Value;
            int.TryParse(idString, out int id);

            var response = await _orderService.GetOrdersByUserId(id);

            return Json(response.Data, JsonRequestBehavior.AllowGet);
        }
    }
}