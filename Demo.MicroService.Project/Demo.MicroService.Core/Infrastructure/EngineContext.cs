using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/8 15:54:52                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Infrastructure                              
*│　类    名： EngineContext                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Infrastructure
{
    public class EngineContext
    {
        private static IServiceProvider _ServiceProvider;
        private static IConfiguration _Configuration;
        private static IServiceCollection _Services;
        public static void AttachService(IServiceCollection services)
        {
            _Services = services;
            _ServiceProvider = _Services.BuildServiceProvider();
        }
        public static void AttachConfiguration(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public static IServiceCollection Services
        {
            get
            {
                return _Services;
            }
        }

        public static IServiceProvider ServiceProvider
        {
            get
            {
                return _ServiceProvider;
            }
        }

        public static IConfiguration Configuration
        {
            get
            {
                return _Configuration;
            }
        }

       

    }
}
