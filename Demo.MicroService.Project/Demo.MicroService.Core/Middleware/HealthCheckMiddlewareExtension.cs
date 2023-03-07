using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述： consul心跳检查                                                   
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/7 15:25:47                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Middleware                              
*│　类    名： HealthCheckMiddlewareExtension                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Middleware
{
    public static class HealthCheckMiddlewareExtension
    {
        /// <summary>
        /// 设置心跳响应
        /// </summary>
        /// <param name="app"></param>
        /// <param name="checkPath">默认是/Health</param>
        /// <returns></returns>
        public static void UseHealthCheckMiddleware(this IApplicationBuilder app, string checkPath = "/Health")
        {
            app.Map(checkPath, applicationBuilder => applicationBuilder.Run(async context =>
            {
                Console.WriteLine($"This is Health Check");
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync("OK");
            }));
        }
    }
}
