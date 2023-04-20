using Demo.MicroService.BusinessDomain.IServices.ITenant.ISystem;
using Demo.MicroService.BusinessModel.DTO.Tenant.System;
using Demo.MicroService.BusinessModel.Model.Tenant.System;
using Demo.MicroService.Core.Application;
using Demo.MicroService.Core.HttpApiExtend;
using Demo.MicroService.Core.Model;
using Demo.MicroService.Core.Utils;
using Demo.MicroService.Core.Utils.Excel;
using Demo.MicroService.Core.ValInject;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Omu.ValueInjecter;
using Serilog.Context;
using SkyApm.Tracing;
using SkyApm.Tracing.Segments;
using System.Diagnostics;
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
        private readonly IEntrySegmentContextAccessor _segContext;
        private readonly Nacos.V2.INacosNamingService _svc;
        private readonly IStringLocalizer<Language> _stringLocalizer;
        private readonly IHttpPollyHelper _httpPollyHelper;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tSysUserService"></param>
        /// <param name="logger"></param>
        public UserController(ITSysUserService tSysUserService, ILogger<UserController> logger, IEntrySegmentContextAccessor segContext, Nacos.V2.INacosNamingService svc, IStringLocalizer<Language> stringLocalizer, IHttpPollyHelper httpPollyHelper)
        {
            _tSysUserService = tSysUserService;
            _logger = logger;
            _segContext = segContext;
            _svc = svc;
            _stringLocalizer = stringLocalizer;
            _httpPollyHelper = httpPollyHelper;
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
        /// 分页查询数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]

        public async Task<PageResult<TSysUser>> PageQueryUser(int pageIndex = 1, int pageSize = 10)
        {
            return await _tSysUserService.PageQueryUser(pageIndex, pageSize);
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
            var result = await _tSysUserService.QueryUser();
            if (result.IsNullT() || result.DataResult.IsNullT())
                return new JsonResult(new ResponseResult() { IsSuccess = false, Message = "导出失败" });
            _logger.LogInformation("客户数据【" + JsonHelper.ObjectToJSON(result.DataResult) + "】");
            var buffer = NpoiUtil.Export(result.DataResult);
            return File(buffer, "application/octet-stream", DateTime.Now.ToString() + "-" + HttpUtility.UrlEncode("客户") + ".xlsx");
        }

        /// <summary>
        /// 客户导入
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        // [Authorize]
        public async Task<IActionResult> ImportSysUser(IFormFile file)
        {
            if (file == null || file.Length <= 0)
                return new JsonResult(new ResponseResult() { IsSuccess = false, Message = "未查询到导入数据" });
            string fileExt = Path.GetExtension(file.FileName).ToLower();
            if (!NpoiUtil.excel.Contains(fileExt))
            {
                return new JsonResult(new ResponseResult() { IsSuccess = false, Message = "文件不存在" });
            }
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "upload", Guid.NewGuid().ToString()) + fileExt;
            using (var st = new MemoryStream())
            {
                file.CopyTo(st);
                var dt = NpoiUtil.Import(st, filepath);
                return new JsonResult(new ResponseResult() { IsSuccess = true, Message = JsonHelper.DataTableToJSON(dt) });
            }

        }
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="tenantCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> QuerySysUser(string name, string password, string tenantCode)
        {
            //var datt=TimeZoneInfo.ConvertTime(DateTime.Now,  TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"), TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time"));
            return await _tSysUserService.QuerySysUser(name, password, tenantCode);
        }



        [HttpGet]
        //[Authorize]
        public async Task<ResponseResult> GetName()
        {
            return await Task.FromResult(new ResponseResult() { Message = "请求成功" });
        }

        /// <summary>
        /// 测试链路追踪
        /// </summary>
        /// <returns></returns>
        [HttpGet("test")]
        public async Task<ResponseResult> TestSkywalking()
        {
            return await Task.FromResult<ResponseResult>(new ResponseResult() { Message = "TestSkywalking" });
        }

        /// <summary>
        /// 获取链接追踪ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("traceId")]
        public async Task<ResponseResult> GetSkywalkingTraceId()
        {
            return await Task.FromResult<ResponseResult>(new ResponseResult() { Value = _segContext.Context.TraceId });
        }

        /// <summary>
        /// 自定义链路调用中的日志信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> SkywalkingLog()
        {
            //获取全局的skywalking的TracId
            var TraceId = _segContext.Context.TraceId;
            Console.WriteLine($"TraceId={TraceId}");
            _segContext.Context.Span.AddLog(LogEvent.Message($"SkywalkingTest1---Worker running at: {DateTime.Now}"));

            Thread.Sleep(1000);

            _segContext.Context.Span.AddLog(LogEvent.Message($"SkywalkingTest1---Worker running at--end: {DateTime.Now}"));

            return await Task.FromResult<ResponseResult>(new ResponseResult() { Message = $" Ok,SkywalkingTest1-TraceId={TraceId}" });
        }

        /// <summary>
        /// 测试nacos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> TestNacos()
        {
            // 这里需要知道被调用方的服务名
            var instance = await _svc.SelectOneHealthyInstance("UserMicroservice", "DEFAULT_GROUP");
            var customerInstance = await _svc.SelectOneHealthyInstance("CustomerMicroservice", "DEFAULT_GROUP");
            //多服务地址
            var ins = await _svc.SelectInstances("UserMicroservice", "DEFAULT_GROUP",true);
            var host = $"{customerInstance.Ip}:{customerInstance.Port}";

            var baseUrl = customerInstance.Metadata.TryGetValue("secure", out _)
                ? $"https://{host}"
                : $"http://{host}";

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                return await Task.FromResult<ResponseResult>(new ResponseResult() { Message = $" baseurl为空" });
            }

            var url = $"{baseUrl}/api/Tenant/Test";

            await Console.Out.WriteLineAsync( "url:"+url);

            using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(url);
                var mess= await result.Content.ReadAsStringAsync();
                return new ResponseResult() { Message = mess };
            }
        }

        /// <summary>
        /// 多语言版本
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="tenantCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResponseResult> Language()
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(DateTime.Parse("2019-07-15 08:30:00"), TimeSpan.Zero);

            return await Task.FromResult(new ResponseResult() { Message = _stringLocalizer.GetString("Name").Value+"；"+ dateTimeOffset });
        }

        #region HttpPolly

        /// <summary>
        /// 测试polly的post请求
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> HttpPollyPost()
        {
            var response = await _httpPollyHelper.PostAsync(HttpEnum.LocalHost, "http://localhost:5726/api/Tenant/Test", "{\"from\": 0,\"size\": 10,\"word\": \"非那雄安\"}");

            return response;
        }
        /// <summary>
        /// 测试polly的get请求
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> HttpPollyGet()
        {
            return await _httpPollyHelper.GetAsync(HttpEnum.LocalHost, "http://localhost:5726/api/Tenant/Test");
        }
        #endregion
    }
}
