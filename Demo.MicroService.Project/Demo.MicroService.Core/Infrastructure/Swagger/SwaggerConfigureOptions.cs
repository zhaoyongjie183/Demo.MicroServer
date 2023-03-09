using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/9 11:46:28                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Infrastructure.Swagger                              
*│　类    名： SwaggerConfigureOptions                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Infrastructure.Swagger
{
    public class SwaggerConfigureOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public SwaggerConfigureOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));

                var modelPrefix = Assembly.GetEntryAssembly()?.GetName().Name + ".Models.";
                var versionPrefix = description.GroupName + ".";
                options.SchemaGeneratorOptions = new SchemaGeneratorOptions { SchemaIdSelector = type => (type.ToString()[(type.ToString().IndexOf("Models.") + 7)..]).Replace(modelPrefix, "").Replace(versionPrefix, "").Replace("`1", "").Replace("+", ".") };
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = Assembly.GetEntryAssembly()?.GetName().Name,
                Version = "v" + description.ApiVersion.ToString(),
                //Description = "",
                //Contact = new OpenApiContact() { Name = "", Email = "" }
            };

            if (description.IsDeprecated)
            {
                info.Description += "此 Api " + info.Version + " 版本已弃用，请尽快升级新版";
            }

            return info;
        }
    }
}
