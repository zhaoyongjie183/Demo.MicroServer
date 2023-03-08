using Nacos.AspNetCore.V2;

var builder = WebApplication.CreateBuilder(args);

#region Nacos配置
// 注册服务到Nacos
builder.Services.AddNacosAspNet(builder.Configuration); //默认节点Nacos
// 添加配置中心
builder.Host.ConfigureAppConfiguration((context, builder) =>
{
    var config = builder.Build();
    builder.AddNacosV2Configuration(config.GetSection("NacosConfig"));
});
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
