using Consul;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/7 15:38:27                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.ConsulExtend                              
*│　类    名： ConsulRegister                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.ConsulExtend
{

    public class ConsulRegister : IConsulRegister
    {
        private readonly ConsulRegisterOptions _consulRegisterOptions;
        private readonly ConsulClientOptions _consulClientOptions;
        public ConsulRegister(IOptionsMonitor<ConsulRegisterOptions> consulRegisterOptions, IOptionsMonitor<ConsulClientOptions> consulClientOptions)
        {
            this._consulRegisterOptions = consulRegisterOptions.CurrentValue;
            this._consulClientOptions = consulClientOptions.CurrentValue;
        }
        public async Task UseConsulRegist()
        {
            using (ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri($"http://{this._consulClientOptions.IP}:{this._consulClientOptions.Port}/");
                c.Datacenter = this._consulClientOptions.Datacenter;
            }))
            {
                await client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = $"{this._consulRegisterOptions.GroupName}-{this._consulRegisterOptions.IP}-{this._consulRegisterOptions.Port}",//唯一Id
                    Name = this._consulRegisterOptions.GroupName,//组名称-Group
                    Address = this._consulRegisterOptions.IP,
                    Port = this._consulRegisterOptions.Port,
                    Tags = new string[] { this._consulRegisterOptions.Tag ?? "Tags is null" },
                    Check = new AgentServiceCheck()
                    {
                        Interval = TimeSpan.FromSeconds(this._consulRegisterOptions.Interval),
                        HTTP = this._consulRegisterOptions.HealthCheckUrl,
                        Timeout = TimeSpan.FromSeconds(this._consulRegisterOptions.Timeout),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(this._consulRegisterOptions.DeregisterCriticalServiceAfter),
                    }
                });
                Console.WriteLine($"{JsonConvert.SerializeObject(this._consulRegisterOptions)} 完成注册");
            }
        }
    }
}
