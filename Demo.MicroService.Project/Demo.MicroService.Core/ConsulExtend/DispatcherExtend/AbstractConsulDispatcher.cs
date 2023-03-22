using Consul;
using Microsoft.Extensions.Options;
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
*│　创建时间：2023/3/22 15:13:36                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.ConsulExtend                              
*│　类    名： AbstractConsulDispatcher                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.ConsulExtend.DispatcherExtend
{
    public abstract class AbstractConsulDispatcher
    {
        protected ConsulClientOptions _ConsulClientOption = null;
        protected KeyValuePair<string, AgentService>[] _CurrentAgentServiceDictionary;
        public AbstractConsulDispatcher(IOptionsMonitor<ConsulClientOptions> options)
        {
            _ConsulClientOption = options.CurrentValue;
        }

        /// <summary>
        /// 负载均衡获取地址
        /// </summary>
        /// <param name="mappingUrl">Consul映射后的地址</param>
        /// <returns></returns>
        public string MapAddress(string mappingUrl)
        {
            Uri uri = new Uri(mappingUrl);
            string serviceName = uri.Host;
            string addressPort = ChooseAddress(serviceName);
            return $"{uri.Scheme}://{addressPort}{uri.PathAndQuery}";
        }

        /// <summary>
        /// 根据服务名字来获取地址
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        protected virtual string ChooseAddress(string serviceName)
        {
            InitAgentServiceDictionary(serviceName);

            int index = GetIndex();
            AgentService agentService = _CurrentAgentServiceDictionary[index].Value;
            return $"{agentService.Address}:{agentService.Port}";
        }

        /// <summary>
        /// 跟Consul交互，获取清单
        /// </summary>
        /// <param name="serviceName"></param>
        private void InitAgentServiceDictionary(string serviceName)
        {
            ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{_ConsulClientOption.IP}:{_ConsulClientOption.Port}/");
                c.Datacenter = _ConsulClientOption.Datacenter;
            });

            //升级consul实例获取
            var entrys = client.Health.Service(serviceName).Result.Response;
            List<KeyValuePair<string, AgentService>> serviceList = new List<KeyValuePair<string, AgentService>>();
            for (int i = 0; i < entrys.Length; i++)
            {
                serviceList.Add(new KeyValuePair<string, AgentService>(i.ToString(), entrys[i].Service));
            }

            _CurrentAgentServiceDictionary = serviceList.ToArray();
        }

        /// <summary>
        /// 根据不同策略  获得不同的索引
        /// </summary>
        /// <returns></returns>
        protected abstract int GetIndex();



    }
}
