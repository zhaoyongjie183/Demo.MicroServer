using Demo.MicroService.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/14 11:55:44                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Application                              
*│　类    名： AspNetUser                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Application
{
    public class AspNetUser:IUser
    {
        private readonly IHttpContextAccessor _accessor;
        public AspNetUser(IHttpContextAccessor accessor, ILogger<AspNetUser> logger)
        {
            _accessor = accessor;
          
        }
        public string Name => GetName();

        private string GetName()
        {
            if (IsAuthenticated() && _accessor.HttpContext.User.Identity.Name.IsNotEmptyOrNull())
            {
                return _accessor.HttpContext.User.Identity.Name;
            }
            else
            {
                if (!string.IsNullOrEmpty(GetToken()))
                {
                    var getNameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
                    return GetUserInfoFromToken(getNameType).FirstOrDefault().ObjToString();
                }
            }

            return "";
        }

        public Guid UserId => GetClaimValueByType("UserID").FirstOrDefault().ToGuid();
        public Guid MTenantId => GetClaimValueByType("MTenantID").FirstOrDefault().ToGuid();

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }


        public string GetToken()
        {
            return _accessor.HttpContext?.Request?.Headers["Authorization"].ObjToString().Replace("Bearer ", "");
        }

        public List<string> GetUserInfoFromToken(string ClaimType)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = "";

            token = GetToken();
            // token校验
            if (token.IsNotEmptyOrNull() && jwtHandler.CanReadToken(token))
            {
                JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);

                return (from item in jwtToken.Claims
                        where item.Type == ClaimType
                        select item.Value).ToList();
            }

            return new List<string>() { };
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            var claims = _accessor.HttpContext.User.Claims.ToList();
            var headers = _accessor.HttpContext.Request.Headers;
            foreach (var header in headers)
            {
                claims.Add(new Claim(header.Key, header.Value));
            }

            return claims;
        }

        public List<string> GetClaimValueByType(string ClaimType)
        {
            return (from item in GetClaimsIdentity()
                    where item.Type == ClaimType
                    select item.Value).ToList();
        }
    }
}
