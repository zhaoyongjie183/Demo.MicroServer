﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Demo.MicroService.AuthenticationCenter.Utility.RSA;
using Demo.MicroService.Core.JWT;

namespace Demo.MicroService.AuthenticationCenter.Utility
{

    public class JWTRSService : IJWTService
    {
        private static Dictionary<string, CurrentUserModel> TokenCache = new Dictionary<string, CurrentUserModel>();

        #region Option注入
        private readonly JWTTokenOptions _JWTTokenOptions;
        public JWTRSService(IOptionsMonitor<JWTTokenOptions> jwtTokenOptions)
        {
            this._JWTTokenOptions = jwtTokenOptions.CurrentValue;
        }
        #endregion


        public string GetToken(CurrentUserModel userModel)
        {
            return this.IssueToken(userModel);
        }

        /// <summary>
        /// 刷新token的有效期问题上端校验
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public string GetTokenByRefresh(string refreshToken)
        {
            if (TokenCache.ContainsKey(refreshToken))
            {
                string token = this.IssueToken(TokenCache[refreshToken], 60);
                return token;
            }
            else
            {
                return "";
            }
        }

        public Tuple<string, string> GetTokenWithRefresh(CurrentUserModel userInfo)
        {
            string token = this.IssueToken(userInfo, 60);//1分钟
            string refreshToken = this.IssueToken(userInfo, 60 * 60 * 24);//24小时
            TokenCache.Add(refreshToken, userInfo);

            return Tuple.Create(token, refreshToken);
        }



        private string IssueToken(CurrentUserModel userModel, int second = 600)
        {
            //string jtiCustom = Guid.NewGuid().ToString();//用来标识 Token
            var claims = new[]
            {
                   //new Claim("scope", "UserMicroservice"),//为了微服务的Scope
                   new Claim(ClaimTypes.Name, userModel.UserName),
                new Claim("EMail", userModel.Mail),
                new Claim("UserID", userModel.TSysUserID.ToString()),
                new Claim("Mobile", userModel.Mobile),
                new Claim("MTenantID", userModel.MTenantID.ToString())//各种信息拼装
            };

            string keyDir = Directory.GetCurrentDirectory();
            if (RSAHelper.TryGetKeyParameters(keyDir, true, out RSAParameters keyParams) == false)
            {
                keyParams = RSAHelper.GenerateAndSaveKey(keyDir);
            }
            var credentials = new SigningCredentials(new RsaSecurityKey(keyParams), SecurityAlgorithms.RsaSha256Signature);

            var token = new JwtSecurityToken(
               issuer: this._JWTTokenOptions.Issuer,
               audience: this._JWTTokenOptions.Audience,
               claims: claims,
               expires: DateTime.Now.AddSeconds(second),//默认10分钟有效期
               notBefore: DateTime.Now,//立即生效  DateTime.Now.AddMilliseconds(30),//30s后有效
               signingCredentials: credentials);
            var handler = new JwtSecurityTokenHandler();
            string tokenString = handler.WriteToken(token);
            return tokenString;
        }
    }
}
