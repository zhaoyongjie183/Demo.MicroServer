using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/10 11:43:12                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.HttpApiExtend                              
*│　类    名： HttpAPIInvokerExtension                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.HttpApiExtend
{
    public static class HttpAPIInvokerExtension
    {
        public static void AddHttpInvoker(this IServiceCollection services, Action<HttpAPIInvokerOptions> action)
        {
            services.Configure<HttpAPIInvokerOptions>(action);//配置给IOC  其他字段用默认值

            services.AddTransient<IHttpAPIInvoker, HttpAPIInvoker>();
            //如果还有其他注册，就一并完成
        }

        public static void AddHttpInvoker(this IServiceCollection services)
        {
            services.AddHttpInvoker(null);
        }
    }
}
