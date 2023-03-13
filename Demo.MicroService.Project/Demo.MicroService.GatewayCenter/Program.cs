using Demo.MicroService.Core.JWT;
using Nacos.AspNetCore.V2;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Ocelot.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ocelot.Administration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.AddLog4Net();
#region Nacos配置
// 注册服务到Nacos
builder.Services.AddNacosAspNet(builder.Configuration, section: "NacosConfig"); //默认节点Nacos
//builder.Host.UseNacosConfig(section: "NacosConfig");
builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
#endregion

// 添加Ocelot对应Nacos扩展
builder.Services.AddOcelot()
    .AddConsul()
    .AddPolly()
    //此配置是用于API更新ocelot配置。
    .AddAdministration("/administration", "CIMSSecret");/*.AddNacosDiscovery()*/;

#region jwt校验  HS
JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind(JWTTokenOptions.JWTTokenOption, tokenOptions);
Console.WriteLine("================="+Newtonsoft.Json.JsonConvert.SerializeObject(tokenOptions));
string authenticationProviderKey = "UserGatewayKey";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Bearer Scheme
.AddJwtBearer(authenticationProviderKey, options =>
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseOcelot();
//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
