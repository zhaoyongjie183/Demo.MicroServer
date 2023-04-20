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
        public static void AddHttpPollySetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            #region Polly策略
            var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<TimeoutRejectedException>() // 若超时则抛出此异常
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10)
            });

            // 为每个重试定义超时策略
            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);
            #endregion


            services.AddHttpClient(HttpEnum.Common.ToString(), c =>
            {
                c.BaseAddress = new Uri("http://localhost");
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(retryPolicy)
            // 将超时策略放在重试策略之内，每次重试会应用此超时策略
            .AddPolicyHandler(timeoutPolicy);

            services.AddSingleton<IHttpPollyHelper, HttpPollyHelper>();
        }
    }
}
