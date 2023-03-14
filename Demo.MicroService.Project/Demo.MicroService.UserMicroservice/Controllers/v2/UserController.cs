using Demo.MicroService.BusinessDomain.IServices.ITenant.ISystem;
using Demo.MicroService.BusinessModel.DTO.Tenant.System;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Utils;
using Demo.MicroService.Core.Utils.Excel;
using Demo.MicroService.Core.ValInject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;
using System.Web;

namespace Demo.MicroService.UserMicroservice.Controllers.v2
{
    /// <summary>
    /// 用户管理API
    /// </summary>
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
        /// 用户新增
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> AddUser([FromBody] TSysUserDTO sysUserDto)
        {
            TSysUser sysUser = new TSysUser();
            sysUser.InjectFrom<CustomValueInjection>(sysUserDto);
            return await _tSysUserService.RegisterUser(sysUser, sysUserDto.TenantCode);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="sysUserDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ResponseResult> UpdateUser([FromBody] TSysUserDTO sysUserDto)
        {
            TSysUser sysUser = new TSysUser();
            sysUser.InjectFrom<CustomValueInjection>(sysUserDto);
            var tsys = await _tSysUserService.QueryUserByName(sysUser.UserName);
            if (tsys.DataResult == null)
                return new ResponseResult() { IsSuccess = false, Message = "客户信息不存在" };
            else
                return await _tSysUserService.UpdateUser(sysUser);
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]

        public async Task<ResponseResult<List<TSysUser>>> QueryUser()
        {
            return await _tSysUserService.QueryUser(); 
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ResponseResult> DeleteUser(Guid id)
        {
            return await _tSysUserService.DeleteUser(id);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ExportSysUser()
        {
            var result= await _tSysUserService.QueryUser();
            if (result.IsNullT() || result.DataResult.IsNullT())
                return new JsonResult(new ResponseResult() { IsSuccess = false, Message = "导出失败" });
            var buffer = NpoiUtil.Export(result.DataResult);
             return File(buffer, "application/octet-stream", DateTime.Now.ToString() + "-" + HttpUtility.UrlEncode("物料") + ".xlsx");
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
