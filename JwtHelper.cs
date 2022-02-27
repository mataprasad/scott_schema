using JwtAuthDemo.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading;

namespace JwtAuthDemo.Core
{
    // Nuget Package - System.IdentityModel.Tokens.Jwt
    public class JwtHelper
    {
        private static readonly byte[] _key = Encoding.UTF8.GetBytes("!z%C*F-JaNdRgUjX");
        private static readonly string _algo = SecurityAlgorithms.HmacSha256;
        public static string GetJwtToken(LoginRequest req)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, req.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, req.UserName));
            claims.Add(new Claim(ClaimTypes.Role, "user"));
            var h = new JwtSecurityTokenHandler();
            var token = h.CreateToken(new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddDays(1),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key), _algo)
            }); ;
            return h.WriteToken(token);
        }

        public static SecurityToken ValidateJwtToken(string token)
        {
            var h = new JwtSecurityTokenHandler();
            h.ValidateToken(token, new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(_key),
                ValidAlgorithms = new[] { _algo }
            }, out var securityToken);
            return securityToken;
        }

        public static void AuthenticateRequest()
        {
            try
            {
                var token = HttpContext.Current.Request.Headers["Authorization"];
                if(string.IsNullOrWhiteSpace(token))
                {
                    token = HttpContext.Current.Request.Cookies["jwt"]?.Value;
                }
                var sec = ValidateJwtToken(token) as JwtSecurityToken;
                var claimId = new ClaimsIdentity(sec.Claims, "jwt", "name", "role");
                var principal = new ClaimsPrincipal(claimId);
                Thread.CurrentPrincipal = principal;
                HttpContext.Current.User = principal;
            }
            catch { }
        }
    }
}
/*
    -- Global.asax.cs
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            JwtHelper.AuthenticateRequest();
        }
    }



*/
