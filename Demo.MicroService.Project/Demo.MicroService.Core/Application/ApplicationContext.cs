using Demo.MicroService.Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/14 11:43:52                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Application                              
*│　类    名： ApplicationContext                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Application
{
    public class ApplicationContext
    {
        public static IServiceProvider RootServices => InternalApp.RootServices;

        /// <summary>
        /// 获取请求上下文
        /// </summary>
        public static HttpContext HttpContext => RootServices?.GetService<IHttpContextAccessor>()?.HttpContext;

        public static IUser User => HttpContext == null ? null : RootServices?.GetService<IUser>();
    }
}
