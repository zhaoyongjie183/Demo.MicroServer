using GraphQL;
using GraphQLWebApi;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Web.Http.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddTransient<IDependencyResolver>(x => new FuncDependencyResolver(x.GetRequiredService));
builder.Services.AddTransient<DemoSchema>();
//builder.Services.AddGraphQL(o => o.ExposeExceptions = true);
//builder.Services.AddGraphTypes(ServiceLifetime.Transient);
builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);
builder.Services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseGraphQL<DemoSchema>();
//app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
