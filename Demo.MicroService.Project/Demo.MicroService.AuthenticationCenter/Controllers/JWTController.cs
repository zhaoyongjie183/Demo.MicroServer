using System.Security.Cryptography;
using Demo.MicroService.AuthenticationCenter.Utility;
using Demo.MicroService.AuthenticationCenter.Utility.RSA;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.HttpApiExtend;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Demo.MicroService.AuthenticationCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ControllerBase
    {
        #region MyRegion
        private ILogger<JWTController> _logger = null;
        private IJWTService _iJWTService = null;
        private readonly IConfiguration _iConfiguration;
        private IHttpAPIInvoker _httpAPIInvoker;
        public JWTController(ILoggerFactory factory,
            ILogger<JWTController> logger,
            IConfiguration configuration
            , IJWTService service
            , IHttpAPIInvoker httpAPIInvoker)
        {
            this._logger = logger;
            this._iConfiguration = configuration;
            this._iJWTService = service;
            this._httpAPIInvoker = httpAPIInvoker;
        }
        #endregion

        [Route("Get")]
        [HttpGet]
        public IEnumerable<int> Get()
        {
            return new List<int>() { 1, 2, 3, 4, 6, 7 };
        }

        [Route("GetKey")]
        [HttpGet]
        public string GetKey()
        {
            string keyDir = Directory.GetCurrentDirectory();
            if (RSAHelper.TryGetKeyParameters(keyDir, false, out RSAParameters keyParams) == false)
            {
                keyParams = RSAHelper.GenerateAndSaveKey(keyDir, false);
            }

            return JsonConvert.SerializeObject(keyParams);
        }

        /// <summary>
        /// 数据库校验
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost]
        public string Login(string name, string password, string TenantCode)
        {
            Console.WriteLine($"This is Login name={name} password={password}  TenantCode={TenantCode}");
            var customerUrl = _iConfiguration["CustomerServiceUrl"];
            var userUrl = _iConfiguration["UserServiceUrl"];
            var result = this._httpAPIInvoker.InvokeApi(customerUrl+"api/Tenant/QueryTenantId?tenantCode=" + TenantCode);
            var tenant = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseResult<Guid>>(result);
            if (tenant.IsNullT() || !tenant.IsSuccess)
                return JsonConvert.SerializeObject(new ResponseResult<string>()
                {
                    IsSuccess = false,
                    Message = "租户不存在",
                    DataResult = ""

                });
            result = this._httpAPIInvoker.InvokeApi(userUrl+"api/User/QuerySysUser?name=" + name + "&password=" + password + "&tenantCode=" + TenantCode,"2");
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseResult<TSysUser>>(result);
            if (user.IsNullT() || !user.IsSuccess)
                return JsonConvert.SerializeObject(new ResponseResult<string>()
                {
                    IsSuccess = false,
                    Message = "用户不存在",
                    DataResult = ""

                });
            CurrentUserModel currentUser = new CurrentUserModel()
            {
                UserName = user.DataResult.UserName,
                TSysUserID=user.DataResult.TSysUserID,
                MTenantID=user.DataResult.MTenantID,
                Mobile=user.DataResult.Mobile,
                Mail =user.DataResult.Mail,
            };

            string token = this._iJWTService.GetToken(currentUser);
            if (!string.IsNullOrEmpty(token))
            {
                return JsonConvert.SerializeObject(new ResponseResult<string>()
                {
                    IsSuccess = true,
                    Message = "Token颁发成功",
                    DataResult = token
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new ResponseResult<string>()
                {
                    IsSuccess = false,
                    Message = "Token获取失败",
                    DataResult = ""
                });
            }
        }


        /// <summary>
        /// 生成Token+RefreshToken
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Route("LoginWithRefresh")]
        [HttpPost]
        public string LoginWithRefresh([FromForm] string name, [FromForm] string password)
        {
            Console.WriteLine($"This is LoginWithRefresh name={name} password={password}");
            if ("Zyj".Equals(name, StringComparison.OrdinalIgnoreCase) && "123456".Equals(password))//应该数据库
            {
                CurrentUserModel currentUser = new CurrentUserModel()
                {
                    //Id = 123,
                    //Account = "xuyang@zhaoxiEdu.Net",
                    //EMail = "57265177@qq.com",
                    //Mobile = "18664876671",
                    //Sex = 1,
                    //Age = 33,
                    //Name = "zyj",
                    //Role = "Admin"
                };

                var tokenPair = this._iJWTService.GetTokenWithRefresh(currentUser);
                if (tokenPair != null && !string.IsNullOrEmpty(tokenPair.Item1))
                {
                    return JsonConvert.SerializeObject(new ResponseResult<string>()
                    {
                        IsSuccess = true,
                        Message = "验证成功",
                        DataResult = tokenPair.Item1,
                        OtherValue = tokenPair.Item2//写在OtherValue
                    });
                }
                else
                {
                    return JsonConvert.SerializeObject(new ResponseResult<string>()
                    {
                        IsSuccess = false,
                        Message = "颁发token失败",
                        DataResult = ""
                    });
                }
            }
            else
            {
                return JsonConvert.SerializeObject(new ResponseResult<string>()
                {
                    IsSuccess = false,
                    Message = "验证失败",
                    DataResult = ""
                });
            }
        }

        [Route("RefreshToken")]
        [HttpPost]
        public async Task<string> RefreshToken([FromForm] string refreshToken)
        {
            await Task.CompletedTask;
            if (!refreshToken.ValidateRefreshToken())
            {
                return JsonConvert.SerializeObject(new ResponseResult<string>()
                {
                    IsSuccess = false,
                    Message = "refreshToken过期了",
                    DataResult = ""
                });
            }
            var token = this._iJWTService.GetTokenByRefresh(refreshToken);
            if (!string.IsNullOrEmpty(token))
            {
                return JsonConvert.SerializeObject(new ResponseResult<string>()
                {
                    IsSuccess = true,
                    Message = "刷新Token成功",
                    DataResult = token,
                    OtherValue = refreshToken//写在OtherValue
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new ResponseResult<string>()
                {
                    IsSuccess = false,
                    Message = "刷新token失败",
                    DataResult = ""
                });
            }
        }
    }
}