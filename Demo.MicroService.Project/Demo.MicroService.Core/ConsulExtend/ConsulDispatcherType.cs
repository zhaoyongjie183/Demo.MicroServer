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
*│　创建时间：2023/3/22 15:17:13                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.ConsulExtend                              
*│　类    名： ConsulDispatcherType                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.ConsulExtend
{
    public enum ConsulDispatcherType
    {
        /// <summary>
        /// 平均
        /// </summary>
        Average = 0,
        /// <summary>
        /// 轮询
        /// </summary>
        Polling = 1,
        /// <summary>
        /// 权重
        /// </summary>
        Weight = 2
    }
}
