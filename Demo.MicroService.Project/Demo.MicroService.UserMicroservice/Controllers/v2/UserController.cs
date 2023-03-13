using Demo.MicroService.BusinessDomain.IServices.ITenant;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Utils;
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

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tSysUserService"></param>
        /// <param name="logger"></param>
        public UserController(ITSysUserService tSysUserService, ILogger<UserController> logger)
        {
            _tSysUserService = tSysUserService;
            _logger = logger;
        }

        /// <summary>
        /// 用户新增或者修改
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> AddOrUpdate([FromBody] TSysUser sysUser)
        {
            var tsys = await _tSysUserService.QueryUser(sysUser.TSysUserID);
            if (tsys.IsNullT())
                return await _tSysUserService.RegisterUser(sysUser);
            else
                return await _tSysUserService.UpdateUser(sysUser);
        }

        /// <summary>
        /// 查询客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult<TSysUser>> QueryUser(Guid id)
        {
            return await _tSysUserService.QueryUser(id); 
        }

        /// <summary>
        /// 删除客户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> DeleteUser(Guid id)
        {
            return await _tSysUserService.DeleteUser(id);
        }
        [HttpGet]
        //[Authorize]
        public async Task<ResponseResult> GetName()
        {
            return await Task.FromResult(new ResponseResult() { Message="请求成功" });
        }
    }
}
