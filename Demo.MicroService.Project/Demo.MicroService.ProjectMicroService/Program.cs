using Nacos.AspNetCore.V2;
using Demo.MicroService.Core.Infrastructure.Swagger;
using Demo.MicroService.Core.Orm;
using SqlSugar;
using Demo.MicroService.Core.ConsulExtend;
using Demo.MicroService.Repository.IRepository;
using Demo.MicroService.Repository.Repository;
using Demo.MicroService.BusinessDomain.IServices;
using Demo.MicroService.BusinessDomain.Services;
using Demo.MicroService.Core.Application;
using Demo.MicroService.Core.Infrastructure;
using Demo.MicroService.Core.HttpApiExtend;
using Demo.MicroService.Core.Middleware;
using Demo.MicroService.Core.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SkyApm.Utilities.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IUser, AspNetUser>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Logging.AddLog4Net();

#region Skywalking配置
builder.Services.AddSkyApmExtensions();
#endregion

#region Nacos配置
// 注册服务到Nacos
builder.Services.AddNacosAspNet(builder.Configuration, section: "NacosConfig"); //默认节点Nacos
//builder.Host.UseNacosConfig(section: "NacosConfig");
builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
#endregion

#region swagger
builder.Services.AddSwaggerGenExt();
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

#region http
builder.Services.AddHttpInvoker(options =>
{
    options.Message = "This is Program's Message";
});
#endregion

#region ioc
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//builder.Services.AddScoped(typeof(ITenantBaseRepository<>), typeof(TenantBaseRepository<>));
builder.Services.AddScoped(typeof(IBaseServices), typeof(BaseServices));

ApplicationManager.RegisterAssembly(builder.Services, "Demo.MicroService.BusinessDomain");
ApplicationManager.RegisterAssembly(builder.Services, "Demo.MicroService.Repository");
EngineContext.AttachService(builder.Services);
EngineContext.AttachConfiguration(builder.Configuration);
#endregion
// Add services to the container.
#region jwt校验  HS
JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind(JWTTokenOptions.JWTTokenOption, tokenOptions);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //JWT有一些默认的属性，就是给鉴权时就可以筛选了
        ValidateIssuer = true,//是否验证Issuer
        ValidateAudience = true,//是否验证Audience
        ValidateLifetime = true,//是否验证失效时间---默认还添加了300s后才过期
        ClockSkew = TimeSpan.FromSeconds(0),//token过期后立马过期
        ValidateIssuerSigningKey = true,//是否验证SecurityKey
        ValidAudience = tokenOptions.Audience,//Audience,需要跟前面签发jwt的设置一致
        ValidIssuer = tokenOptions.Issuer,//Issuer，这两项和前面签发jwt的设置一致
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),//拿到SecurityKey
    };
});
#endregion

builder.Services.AddControllers();


var app = builder.Build();
app.ConfigureApplication();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

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
