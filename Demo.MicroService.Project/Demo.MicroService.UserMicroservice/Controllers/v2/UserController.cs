using Demo.MicroService.BusinessDomain.IServices.ITenant;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MicroService.UserMicroservice.Controllers.v2
{
    [ApiVersion("2")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITSysUserService _tSysUserService;
        private readonly ILogger<UserController> _logger;

        public UserController(ITSysUserService tSysUserService, ILogger<UserController> logger)
        {
            _tSysUserService = tSysUserService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ResponseResult> AddOrUpdate([FromBody] TSysUser sysUser)
        {
            _logger.LogInformation("sdsdsds");
           // throw new Exception("异常发生了");
            return await _tSysUserService.RegisterUser(sysUser);
            //JsonResult result = new JsonResult(new ResponseResult<TSysUser>() { DataResult = sysUser, IsSuccess = true, Message = "新增成功" });
            //return result;
        }
    }
}
