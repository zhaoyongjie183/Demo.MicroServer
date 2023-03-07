/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/7 15:40:19                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.ConsulExtend                              
*│　类    名： ConsulClientOptions                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.ConsulExtend
{
    public class ConsulClientOptions
    {
        public string? IP { get; set; }
        public int Port { get; set; }
        public string? Datacenter { get; set; }
    }
}
