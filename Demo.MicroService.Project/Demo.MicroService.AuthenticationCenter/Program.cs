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
#region Nacos����
// ע�����Nacos
//builder.Services.AddNacosAspNet(builder.Configuration, section: "NacosConfig"); //Ĭ�Ͻڵ�Nacos
////builder.Host.UseNacosConfig(section: "NacosConfig");
//builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
#endregion
#region HS256 �Գƿ������
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
