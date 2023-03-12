using Demo.MicroService.AuthenticationCenter.Utility;
using Demo.MicroService.Core.JWT;
using Microsoft.AspNetCore.Identity;
using Nacos.AspNetCore.V2;
using Nacos.V2.Naming.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region Nacos配置
// 注册服务到Nacos
//builder.Services.AddNacosAspNet(builder.Configuration, section: "NacosConfig"); //默认节点Nacos
////builder.Host.UseNacosConfig(section: "NacosConfig");
//builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
#endregion
#region HS256 对称可逆加密
builder.Services.AddScoped<IJWTService, JWTHSService>();
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection(JWTTokenOptions.JWTTokenOption));
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
