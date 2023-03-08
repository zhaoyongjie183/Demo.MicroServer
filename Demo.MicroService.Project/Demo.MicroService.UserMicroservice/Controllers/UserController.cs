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

        public UserController(ITSysUserService tSysUserService)
        {
            this._tSysUserService = tSysUserService;
        }

        [HttpPost]
        public async Task<JsonResult> AddOrUpdate([FromBody] TSysUser sysUser)
        {
            _tSysUserService.Insert(sysUser);
            JsonResult result = new JsonResult(new ResponseResult<TSysUser>() { DataResult = sysUser, IsSuccess = true, Message = "新增成功" });
            return result;
        }
    }
}
