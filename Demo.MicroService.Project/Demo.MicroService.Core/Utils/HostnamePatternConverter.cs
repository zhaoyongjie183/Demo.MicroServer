using log4net.Util;
using System.Net;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/9 10:08:25                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Utils                              
*│　类    名： HostnamePatternConverter                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core
{
    public class HostnamePatternConverter : PatternConverter
    {
        protected override async void Convert(TextWriter writer, object state)
        {
            var hostname = (string)state;
            await writer.WriteAsync(hostname);
        }
    }
}
