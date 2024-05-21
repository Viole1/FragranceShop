using GS_Parfum.DAL.DbContexts;
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
    public class HomeController : Controller
    {
        private readonly ProductDbContext _db;
        private readonly CartDbContext _cart;
        private readonly UserDbContext _user;
        private readonly OrderDbContext _order;
        private readonly IUserService _userService;

        public HomeController(ProductDbContext db, CartDbContext cart, UserDbContext user, OrderDbContext order, IUserService userService)
        {
            _db = db;
            _cart = cart;
            _user = user;
            _order = order;
            _userService = userService;
        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public new async Task<ActionResult> Profile()
        {
            var token = HttpContext.Request.Cookies["AuthToken"].Value;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var userId = jwtToken.Claims.ToList()[0].Value;
            int.TryParse(userId, out int intUserId);
            var user = await _userService.GetUserById(intUserId);

            return View(user.Data);
        }
    }
}