using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GS_Parfum.Domain.Entity;
using GS_Parfum.Domain.Entity.Product;
using GS_Parfum.Domain.Enum;
using GS_Parfum.Service.Implementations;
using GS_Parfum.Service.Interfaces;

namespace GS_Parfum.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IProductService _productService;
        public ReviewController(ReviewService reviewService, ProductService productService)
        {
            _reviewService = reviewService;
            _productService = productService;
        }

        [HttpPost]
        public async Task<ActionResult> AddReview(Review review)
        {
            var token = HttpContext.Request.Cookies["AuthToken"].Value;

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var idString = jwtToken.Claims.ToList()[0].Value;
            int.TryParse(idString, out int id);
            review.UserId = id;

            await _reviewService.AddReview(review);
            await _productService.UpdateRatings(review);

            return RedirectToAction("GetProduct", "Product", new { id = review.ProductId });
        }

        [HttpGet]
        public async Task<JsonResult> GetReviews(int productId)
        {
            var response = await _reviewService.GetReviewsByProductId(productId);
            if (response.ResponseStatus == ResponseStatus.OK)
                {
                    return Json(response.Data, JsonRequestBehavior.AllowGet);
                }
            else
                {
                    return Json(new { error = response.Description }, JsonRequestBehavior.AllowGet);
                }
        }
    }
}