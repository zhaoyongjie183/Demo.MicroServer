using Demo.MicroService.Core.HttpApiExtend;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：polly服务治理                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/4/20 9:00:26                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： CIMS.Common.ServiceExtensions                              
*│　类    名： HttpPollySetup                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Middleware
{
    public static class HttpPollySetup
    {
        /// <summary>
        /// 添加polly服务治理
        /// </summary>
        /// <param name="services"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static async void AddHttpPollySetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            #region Polly策略
            //重试策略
            var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<Exception>() // 若超时则抛出此异常
            .RetryAsync(3);

            //超时策略
            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(5, TimeoutStrategy.Pessimistic, (x, y, z) => { 
                Console.WriteLine("超时了"); 
                return Task.CompletedTask;
            });

            //熔断
            var circuitPoliy = Policy.Handle<Exception>()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10), onBreak: (ex, ts) => 
            {
                Console.WriteLine($"熔断器打开 熔断.");
                
            }, onReset: () => 
            {
                Console.WriteLine("熔断器关闭，流量正常通行");
            }, onHalfOpen: () => 
            {
                Console.WriteLine("熔断时间到，熔断器半开，放开部分流量进入");
            });

            //降级
            var fallbackPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .FallbackAsync(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent("Fallback response")
            }
            , x =>
            {
                Console.WriteLine($"进行了服务降级 --");
                return Task.CompletedTask;
            }).WrapAsync(circuitPoliy.WrapAsync(retryPolicy).WrapAsync(timeoutPolicy));

            #endregion

            services.AddHttpClient(HttpEnum.LocalHost.ToString(), c =>
            {
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            }).AddPolicyHandler(fallbackPolicy)
            //   .AddPolicyHandler(retryPolicy)
            //.AddPolicyHandler(circuitPoliy)
           //.AddPolicyHandler(circuitPoliy)
           // 将超时策略放在重试策略之内，每次重试会应用此超时策略
           ;

            services.AddHttpClient(HttpEnum.Common.ToString(), c =>
            {
                c.BaseAddress = new Uri("http://localhost");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(retryPolicy)
           
            //.AddPolicyHandler(circuitPoliy)
            // 将超时策略放在重试策略之内，每次重试会应用此超时策略
            .AddPolicyHandler(timeoutPolicy);

            services.AddSingleton<IHttpPollyHelper, HttpPollyHelper>();
        }
    }
}
