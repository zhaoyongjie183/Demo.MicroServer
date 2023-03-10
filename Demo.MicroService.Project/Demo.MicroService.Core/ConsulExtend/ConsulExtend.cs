﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/10 13:57:12                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.ConsulExtend                              
*│　类    名： ConsulExtend                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.ConsulExtend
{
    public static class ConsulExtend
    {
        /// <summary>
        /// 完成注册
        /// </summary>
        /// <param name="services"></param>
        public static void AddConsulRegister(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddTransient<IConsulRegister, ConsulRegister>();//完成IOC注册
            services.Configure<ConsulClientOptions>(configuration.GetSection(ConsulClientOptions.ConsulClientOption));
            services.Configure<ConsulRegisterOptions>(configuration.GetSection(ConsulRegisterOptions.ConsulRegisterOption));
        }
    }
}
