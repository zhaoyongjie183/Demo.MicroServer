using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Consul;
using Microsoft.Extensions.Options;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023-3-22 15:14:48                        
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.ConsulExtend                              
*│　类    名： PollingDispatcher                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.ConsulExtend.DispatcherExtend
{
    /// <summary>
    /// 轮询
    /// </summary>
    public class PollingDispatcher : AbstractConsulDispatcher
    {
        #region Identity
        private static int _iTotalCount = 0;
        private static int iTotalCount
        {
            get
            {
                return _iTotalCount;
            }
            set
            {
                _iTotalCount = value >= Int32.MaxValue ? 0 : value;
            }
        }

        public PollingDispatcher(IOptionsMonitor<ConsulClientOptions> consulClientOption) : base(consulClientOption)
        {
        }
        #endregion

        /// <summary>
        /// 轮询
        /// </summary>
        /// <param name="serviceCount"></param>
        /// <returns></returns>
        protected override int GetIndex()
        {
            return iTotalCount++ % base._CurrentAgentServiceDictionary.Length;
        }
    }
}
