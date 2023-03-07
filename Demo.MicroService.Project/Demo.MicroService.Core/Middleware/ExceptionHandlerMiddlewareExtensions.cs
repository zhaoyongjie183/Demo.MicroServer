
using Demo.MicroService.Core.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述： 异常处理中间件                                                   
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/7 16:05:22                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Middleware                              
*│　类    名： UseExceptionHandlerMiddleware                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Middleware
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseExceptionHandler(appBuilder => appBuilder.Use(async (context, next) =>
            {
                var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
                //when authorization has failed, should retrun a json message to client
                if (error != null && error.Error != null)
                {
                    string msg = error.Error.GetType().Name == typeof(UserThrowException).Name ? error.Error.Message : "请求错误";
                    if (context.Request.IsAjax())
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(
                          new { IsSuccess = false, OperationDesc = msg }
                        ));
                    }
                    else
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/html; charset=utf-8";
                        await context.Response.WriteAsync(msg);
                    }
                    //var log=appBuilder.ApplicationServices.GetService<ILogger>();
                    //log.LogInformation("异常记录=>请求路径:{0}", context.Request.Path);
                }
                //when no error, do next.
                else await next();
            }));
        }

        public static bool IsAjax(this HttpRequest req)
        {
            bool result = false;

            var xreq = req.Headers.ContainsKey("x-requested-with");
            if (xreq)
            {
                result = req.Headers["x-requested-with"] == "XMLHttpRequest";
            }

            return result;
        }
    }
}
