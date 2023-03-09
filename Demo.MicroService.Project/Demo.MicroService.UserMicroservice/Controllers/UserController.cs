using Demo.MicroService.BusinessDomain.IServices.ITenant;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MicroService.UserMicroservice.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ITSysUserService _tSysUserService;
        private readonly ILogger<UserController> _logger;

        public UserController(ITSysUserService tSysUserService, ILogger<UserController> logger)
        {
            this._tSysUserService = tSysUserService;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<JsonResult> AddOrUpdate([FromBody] TSysUser sysUser)
        {
            _logger.LogInformation("sdsdsds");
            throw new Exception("异常发生了");
            //_tSysUserService.Insert(sysUser);
            JsonResult result = new JsonResult(new ResponseResult<TSysUser>() { DataResult = sysUser, IsSuccess = true, Message = "新增成功" });
            return result;
        }
    }
}
