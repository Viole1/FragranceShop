using GS_Parfum.Domain.Entity.Cart;
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
    public class CartController : Controller
    {
        // GET: Cart
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public ActionResult AddCartItem()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddCartItem(CartItem cartItem)
        {
            var token = HttpContext.Request.Cookies["AuthToken"].Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var idString = jwtToken.Claims.ToList()[0].Value;
            int.TryParse(idString, out int id);
            cartItem.UserId = id;

            await _cartService.AddItem(cartItem);
            return RedirectToAction("Cart");
        }
        public async Task<ActionResult> Cart()
        {
            var token = HttpContext.Request.Cookies["AuthToken"].Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var idString = jwtToken.Claims.ToList()[0].Value;
            int.TryParse(idString, out int id);
            var response = await _cartService.GetCartByUserId(id);

            return View(response.Data);
        }
    }
}