using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/9 21:53:32                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Infrastructure.Swagger                              
*│　类    名： SwaggerExtension                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Infrastructure.Swagger
{
    public static class SwaggerExtension
    {
        /// <summary>
        /// Swagger注册方法
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSwaggerGenExt(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetEntryAssembly().GetName().Name}.xml"), true);
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //在header中添加token，传递到后台
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.OperationFilter<SwaggerOperationFilter>();
                //Jwt Bearer 认证，必须是 oauth2
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                //添加全局安全条件
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"}
                        },new string[] { }
                    }
                });
            });
            builder.Services.AddApiVersioning(options =>
            {
                //通过Header向客户端通报支持的版本
                options.ReportApiVersions = true;

                //允许不加版本标记直接调用接口
                options.AssumeDefaultVersionWhenUnspecified = true;

                //接口默认版本
                //options.DefaultApiVersion = new ApiVersion(1,0);

                //如果未加版本标记默认以当前最高版本进行处理
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);

                //配置为从 Header 传入 api-version
                options.ApiVersionReader = new HeaderApiVersionReader("api-version");

                //配置为从 Query 传入 api-version
                //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");

            });

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            
        }

        /// <summary>
        /// Swagger使用方法
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerExt(this WebApplication app)
        {
            #region 使用Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }

                options.RoutePrefix = "swagger";
            });
            //使用自定义UI界面
            app.UseKnife4UI(options =>
            {
                var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
                //options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
            #endregion
        }
    }
}
