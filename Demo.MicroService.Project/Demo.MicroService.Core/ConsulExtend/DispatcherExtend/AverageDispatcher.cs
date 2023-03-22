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
*│　创建时间：2023-3-22 15:14:41                           
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.ConsulExtend                              
*│　类    名： AverageDispatcher                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.ConsulExtend.DispatcherExtend
{
    /// <summary>
    /// 平均
    /// </summary>
    public class AverageDispatcher : AbstractConsulDispatcher
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

        private ConsulClientOptions _ConsulClientOption = null;

        public AverageDispatcher(IOptionsMonitor<ConsulClientOptions> consulClientOption) : base(consulClientOption)
        {
        }
        #endregion

        /// <summary>
        /// 平均
        /// </summary>
        /// <returns></returns>
        protected override int GetIndex()
        {
            return new Random(iTotalCount++).Next(0, base._CurrentAgentServiceDictionary.Length);
        }
    }
}
