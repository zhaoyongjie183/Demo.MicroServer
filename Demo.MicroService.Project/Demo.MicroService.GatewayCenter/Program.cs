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
#region Nacos����
// ע�����Nacos
builder.Services.AddNacosAspNet(builder.Configuration, section: "NacosConfig"); //Ĭ�Ͻڵ�Nacos
//builder.Host.UseNacosConfig(section: "NacosConfig");
builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
#endregion

// ���Ocelot��ӦNacos��չ
builder.Services.AddOcelot()
    .AddConsul()
    .AddPolly()
    //������������API����ocelot���á�
    .AddAdministration("/administration", "CIMSSecret");/*.AddNacosDiscovery()*/;

#region jwtУ��  HS
JWTTokenOptions tokenOptions = new JWTTokenOptions();
builder.Configuration.Bind(JWTTokenOptions.JWTTokenOption, tokenOptions);
Console.WriteLine("================="+Newtonsoft.Json.JsonConvert.SerializeObject(tokenOptions));
string authenticationProviderKey = "UserGatewayKey";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Bearer Scheme
.AddJwtBearer(authenticationProviderKey, options =>
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
