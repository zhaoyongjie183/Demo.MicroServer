/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/7 15:39:45                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.ConsulExtend                              
*│　类    名： ConsulRegisterOptions                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.ConsulExtend
{
    public class ConsulRegisterOptions
    {
        /// <summary>
        /// 服务自身IP
        /// </summary>
        public string? IP { get; set; }
        /// <summary>
        /// 服务自身Port
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 组名称
        /// </summary>
        public string? GroupName { get; set; }
        /// <summary>
        /// 心跳检查地址
        /// </summary>
        public string? HealthCheckUrl { get; set; }
        /// <summary>
        /// 心跳频率
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// 心跳超时
        /// </summary>
        public int Timeout { get; set; }
        /// <summary>
        /// 移除延迟时间
        /// </summary>
        public int DeregisterCriticalServiceAfter { get; set; }
        /// <summary>
        /// 标签，额外信息，用于权重
        /// </summary>
        public string? Tag { get; set; }
    }
}
