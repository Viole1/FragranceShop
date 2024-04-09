using GS_Parfum.Domain.Enum;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GS_Parfum.Util
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private Role _requiredRole;

        public CustomAuthorizeAttribute(Role requiredRole)
        {
            _requiredRole = requiredRole;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authCookie = httpContext.Request.Cookies["AuthToken"];
            if (authCookie != null)
            {
                string token = authCookie.Value;
                // Проверка токена на корректность
                if (IsValidToken(token))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                    if (jwtToken != null && jwtToken.Claims != null)
                    {
                        var roleClaim = jwtToken.Claims.ToList()[1].Value;
                        var role = (Role)Enum.Parse(typeof(Role), roleClaim);
                        if (role == _requiredRole || role == Role.ROLE_ADMIN)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsValidToken(string token)
        {
            return !string.IsNullOrEmpty(token);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/User/Login");
        }
    }
}