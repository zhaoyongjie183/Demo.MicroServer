using Demo.MicroService.BusinessDomain.IServices;
using Demo.MicroService.BusinessDomain.Services;
using Demo.MicroService.Core.Application;
using Demo.MicroService.Core.Infrastructure;
using Demo.MicroService.Core.Orm;
using Demo.MicroService.Repository.IRepository;
using Demo.MicroService.Repository.Repository;
using Nacos.AspNetCore.V2;
using SqlSugar;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddLog4Net();
#region Nacos����
// ע�����Nacos
builder.Services.AddNacosAspNet(builder.Configuration, section: "NacosConfig"); //Ĭ�Ͻڵ�Nacos
//builder.Host.UseNacosConfig(section: "NacosConfig");
builder.Configuration.AddNacosV2Configuration(builder.Configuration.GetSection("NacosConfig"));
#endregion

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IBaseServices<>), typeof(BaseServices<>));

#region Orm
var sqlSugarConfig = SqlSugarConfig.GetConnectionString(builder.Configuration);
builder.Services.AddSqlSugarClient<SqlSugarClient>(config =>
{
    config.ConnectionString = sqlSugarConfig.Item2;
    config.DbType = sqlSugarConfig.Item1;
    config.IsAutoCloseConnection = true;
    config.InitKeyType = InitKeyType.Attribute;
    //config.IsShardSameThread = true;
});
#endregion

ApplicationManager.RegisterAssembly(builder.Services, "Demo.MicroService.BusinessDomain");
ApplicationManager.RegisterAssembly(builder.Services, "Demo.MicroService.Repository");
EngineContext.AttachService(builder.Services);
EngineContext.AttachConfiguration(builder.Configuration);

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



