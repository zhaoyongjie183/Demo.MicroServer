using Nacos.AspNetCore.V2;
using Demo.MicroService.Core.Infrastructure.Swagger;
using Demo.MicroService.Core.Orm;
using SqlSugar;
using Demo.MicroService.Core.ConsulExtend;
using Demo.MicroService.Core.Application;
using Demo.MicroService.Core.Infrastructure;
using Demo.MicroService.Repository.IRepository;
using Demo.MicroService.Repository.Repository;
using Demo.MicroService.BusinessDomain.IServices;
using Demo.MicroService.BusinessDomain.Services;
using Demo.MicroService.Core.Middleware;
using Demo.MicroService.Core.HttpApiExtend;
using SkyApm.Utilities.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLog4Net();
#region Nacos配置
// 注册服务到Nacos
builder.Services.AddNacosAspNet(builder.Configuration, section: "NacosConfig"); //默认节点Nacos
//builder.Host.UseNacosConfig(section: "NacosConfig");
builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
#endregion

#region Skywalking配置
builder.Services.AddSkyApmExtensions();
#endregion
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
#region swagger
builder.AddSwaggerGenExt();
#endregion
#region Orm
var sqlSugarConfig = SqlSugarConfig.GetConnectionString(builder.Configuration);
builder.Services.AddSqlSugarClient<SqlSugarClient>(config =>
{
    config.ConnectionString = sqlSugarConfig.Item2;
    config.DbType = sqlSugarConfig.Item1;
    config.IsAutoCloseConnection = true;
    config.InitKeyType = InitKeyType.Attribute;
});
#endregion

#region consul
builder.Services.AddConsulRegister(builder.Configuration);
builder.Services.AddConsulDispatcher(ConsulDispatcherType.Polling);
#endregion
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//builder.Services.AddScoped(typeof(ITenantBaseRepository<>), typeof(TenantBaseRepository<>));
builder.Services.AddScoped(typeof(IBaseServices), typeof(BaseServices));

ApplicationManager.RegisterAssembly(builder.Services, "Demo.MicroService.BusinessDomain");
ApplicationManager.RegisterAssembly(builder.Services, "Demo.MicroService.Repository");
EngineContext.AttachService(builder.Services);
EngineContext.AttachConfiguration(builder.Configuration);

#region http
builder.Services.AddHttpInvoker(options =>
{
    options.Message = "This is Program's Message";
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
#region swaggerui
app.UseSwaggerExt();
#endregion


#region Consul注册
app.UseHealthCheckMiddleware("/Api/Health/Index");//心跳请求响应
app.Services.GetService<IConsulRegister>()!.UseConsulRegist();
#endregion
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
