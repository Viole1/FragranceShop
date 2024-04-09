using GS_Parfum.Domain.Request;
using GS_Parfum.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GS_Parfum.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            var loginRequest = new LoginRequest();
            return View(loginRequest);
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                var loginResponse = await _userService.Login(model);
                if (loginResponse.Data != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    var authCookie = new HttpCookie("AuthToken", loginResponse.Token);
                    authCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(authCookie);

                    return RedirectToAction("GetProducts", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect.");
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Register()
        {
            var registerRequestDTO = new RegistrationRequest();
            return View(registerRequestDTO);
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegistrationRequest model)
        {

            if (ModelState.IsValid) 
                await _userService.Register(model);
            return View();
        }
    }
}