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
*│　创建时间：2023/3/10 11:42:50                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.HttpApiExtend                              
*│　类    名： HttpAPIInvoker                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.HttpApiExtend
{
    public class HttpAPIInvoker : IHttpAPIInvoker
    {
        private readonly HttpAPIInvokerOptions _httpInvokerOptions;
        public HttpAPIInvoker(IOptions<HttpAPIInvokerOptions> options)
        {
            this._httpInvokerOptions = options.Value;
        }


        /// <summary>
        /// 给个URL，然后发起Http请求，拿到结果
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string InvokeApi(string url,string version="1")
        {
            Console.WriteLine(this._httpInvokerOptions.Message);
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);
                message.Headers.Add("api-version", version);

                var result = httpClient.SendAsync(message).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
    }
}
