using Demo.MicroService.BusinessDomain.IServices.IMange;
using Demo.MicroService.BusinessModel.DTO.Mange;
using Demo.MicroService.BusinessModel.Model.Mange;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.ValInject;
using Microsoft.AspNetCore.Mvc;
using Omu.ValueInjecter;

namespace Demo.MicroService.CustomerMicroService.Controllers.v1
{
    /// <summary>
    /// 客户管理API
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("1")]
    public class TenantController : ControllerBase
    {
        private readonly IMangeTenantService _mangeTenantService;
        private readonly ILogger<TenantController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tSysUserService"></param>
        /// <param name="logger"></param>
        public TenantController(IMangeTenantService mangeTenantService, ILogger<TenantController> logger)
        {
            _mangeTenantService = mangeTenantService;
            _logger = logger;
        }

        /// <summary>
        /// 客户新增
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> AddTenant([FromBody] MangeTenantDTO mangeTenantDTO)
        {
            MangeTenant mangeTenant = new MangeTenant();
            mangeTenant.InjectFrom<CustomValueInjection>(mangeTenantDTO);
            return await _mangeTenantService.AddCustomer(mangeTenant);
           
        }

        /// <summary>
        /// 客户修改
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResponseResult> UpdateTenant([FromBody] MangeTenantDTO mangeTenantDTO)
        {
            MangeTenant mangeTenant = new MangeTenant();
            mangeTenant.InjectFrom<CustomValueInjection>(mangeTenantDTO);
            return await _mangeTenantService.UpdateCustomer(mangeTenant);

        }

        /// <summary>
        /// 查询租户信息
        /// </summary>
        /// <param name="tenantCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> QueryTenantId(string tenantCode)
        { 
            return await _mangeTenantService.QueryTenantId(tenantCode);
        }

        /// <summary>
        /// 查询租户信息
        /// </summary>
        /// <param name="tenantCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> QueryTenantConneString(Guid tenantId)
        {
            return await _mangeTenantService.QueryTenantConneString(tenantId);
        }
    }
}
