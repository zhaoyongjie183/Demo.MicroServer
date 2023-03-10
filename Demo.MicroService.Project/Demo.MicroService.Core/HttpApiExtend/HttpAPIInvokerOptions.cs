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
*│　创建时间：2023/3/10 11:43:29                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.HttpApiExtend                              
*│　类    名： HttpAPIInvokerOptions                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.HttpApiExtend
{
    public class HttpAPIInvokerOptions
    {
        public string? Message { get; set; } = "HttpAPIInvokerOptions Message";
        public bool IsUseHttpClient { get; set; } = true;
    }
}
