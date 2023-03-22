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

#region Skywalking����
builder.Services.AddSkyApmExtensions();
#endregion

#region Nacos����
// ע�����Nacos
builder.Services.AddNacosAspNet(builder.Configuration, section: "NacosConfig"); //Ĭ�Ͻڵ�Nacos
//builder.Host.UseNacosConfig(section: "NacosConfig");
builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
#endregion

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
#region jwtУ��  HS
JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind(JWTTokenOptions.JWTTokenOption, tokenOptions);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //JWT��һЩĬ�ϵ����ԣ����Ǹ���Ȩʱ�Ϳ���ɸѡ��
        ValidateIssuer = true,//�Ƿ���֤Issuer
        ValidateAudience = true,//�Ƿ���֤Audience
        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��---Ĭ�ϻ������300s��Ź���
        ClockSkew = TimeSpan.FromSeconds(0),//token���ں��������
        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
        ValidAudience = tokenOptions.Audience,//Audience,��Ҫ��ǰ��ǩ��jwt������һ��
        ValidIssuer = tokenOptions.Issuer,//Issuer���������ǰ��ǩ��jwt������һ��
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey)),//�õ�SecurityKey
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

#region Consulע��
app.UseHealthCheckMiddleware("/Api/Health/Index");//����������Ӧ
app.Services.GetService<IConsulRegister>()!.UseConsulRegist();
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
