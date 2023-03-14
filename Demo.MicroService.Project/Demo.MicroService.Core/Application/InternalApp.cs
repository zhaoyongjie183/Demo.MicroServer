using Demo.MicroService.Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
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
*│　创建时间：2023/3/14 11:42:47                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Application                              
*│　类    名： InternalApp                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Application
{
    public static class InternalApp
    {
        /// <summary>根服务</summary>
        public static IServiceProvider RootServices;

        public static void ConfigureApplication(this WebApplication app)
        {
            app.Lifetime.ApplicationStarted.Register(() => { InternalApp.RootServices = EngineContext.ServiceProvider; });

            app.Lifetime.ApplicationStopped.Register(() => { InternalApp.RootServices = null; });
        }
    }
}
