using Demo.MicroService.Core.Utils;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.Loader;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 11:03:23                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Application                              
*│　类    名： ApplicationManager                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Application
{
    public static class ApplicationManager
    {
        /// <summary>
        /// 单个注册DI
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <param name="injection"></param>
        public static void Register<TService, TImplementation>(IServiceCollection services, ServiceLifetime injection = ServiceLifetime.Scoped)
        {
            //ServiceCollectionDescriptorExtensions
            switch (injection)
            {
                case ServiceLifetime.Scoped:
                    services.AddScoped(typeof(TService), typeof(TImplementation));
                    break;

                case ServiceLifetime.Singleton:
                    services.AddSingleton(typeof(TService), typeof(TImplementation));
                    break;

                case ServiceLifetime.Transient:
                    services.AddTransient(typeof(TService), typeof(TImplementation));
                    break;
            }
        }
        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="service">IServiceCollection</param>
        /// <param name="assemblyName">程序集的名称</param>
        /// <param name="injection">生命周期</param>
        public static void RegisterAssembly(IServiceCollection services, string assemblyName, ServiceLifetime injection = ServiceLifetime.Scoped)
        {
            CheckNull.ArgumentIsNullException(services, nameof(services));
            CheckNull.ArgumentIsNullException(assemblyName, nameof(assemblyName));
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
            if (assembly.IsNullT())
            {
                throw new ArgumentNullException($"\"{assemblyName}\".dll不存在");
            }
            var types = assembly.GetTypes().Where(o =>(typeof(IDependency).IsAssignableFrom(o)) && !o.IsInterface).ToList();

            foreach (var type in types)
            {
                var faces = type.GetInterfaces().Where(o => o.Name != nameof(IDependency) && !o.Name.Contains("Base")).ToArray();
                if (faces.Any())
                {
                    var interfaceType = faces.FirstOrDefault();
                    switch (injection)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(interfaceType, type);
                            break;

                        case ServiceLifetime.Singleton:
                            services.AddSingleton(interfaceType, type);
                            break;

                        case ServiceLifetime.Transient:
                            services.AddTransient(interfaceType, type);
                            break;
                    }
                }
            }
        }

        
    }
}
