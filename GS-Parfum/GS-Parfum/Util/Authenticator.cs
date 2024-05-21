using GS_Parfum.Domain.Enum;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;

namespace GS_Parfum.Util
{
    public class Authenticator
    {
        public static bool IsUserAuthorized(HttpRequestBase request)
        {
            bool isAuthorized = false;
            var authCookie = request.Cookies["AuthToken"];
            if (authCookie != null)
            {
                string token = authCookie.Value;
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken != null && jwtToken.Claims != null)
                {
                    isAuthorized = true;
                }
            }
            return isAuthorized;
        }
        public static bool IsUserAdmin(HttpRequestBase request)
        {
            bool isAdmin = false;
            var authCookie = request.Cookies["AuthToken"];
            if (authCookie != null)
            {
                string token = authCookie.Value;
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken != null && jwtToken.Claims != null)
                {
                    var roleClaim = jwtToken.Claims.ToList()[1].Value;
                    var role = (Role)Enum.Parse(typeof(Role), roleClaim);
                    if (role == Role.ROLE_ADMIN)
                    {
                        isAdmin = true;
                    }
                }
            }
            return isAdmin;
        }
    }
}