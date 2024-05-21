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

        public HomeController(ProductDbContext db, CartDbContext cart, UserDbContext user, OrderDbContext order)
        {
            _db = db;
            _cart = cart;
            _user = user;
            _order = order;
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
    }
}