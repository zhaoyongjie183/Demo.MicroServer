using Demo.MicroService.BusinessDomain.IServices.ITenant;
using Demo.MicroService.BusinessModel.DTO.Tenant;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Utils;
using Demo.MicroService.Core.ValInject;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;

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
        public async Task<ResponseResult> AddOrUpdate([FromBody] TSysUserDTO sysUserDto)
        {
            TSysUser sysUser = new TSysUser();
            sysUser.InjectFrom<CustomValueInjection>(sysUserDto);
            var tsys = await _tSysUserService.QueryUser(sysUser.TSysUserID);
            if (tsys.DataResult==null)
                return await _tSysUserService.RegisterUser(sysUser,sysUserDto.TenantCode);
            else
                return await _tSysUserService.UpdateUser(sysUser);
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult<TSysUser>> QueryUser(Guid id)
        {
            return await _tSysUserService.QueryUser(id); 
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> DeleteUser(Guid id)
        {
            return await _tSysUserService.DeleteUser(id);
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="tenantCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> QuerySysUser(string name,string password,string tenantCode)
        {
            return await _tSysUserService.QuerySysUser(name, password, tenantCode);
        }



        [HttpGet]
        //[Authorize]
        public async Task<ResponseResult> GetName()
        {
            return await Task.FromResult(new ResponseResult() { Message="请求成功" });
        }
    }
}
