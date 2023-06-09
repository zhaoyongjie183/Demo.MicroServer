﻿/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/10 11:42:32                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.HttpApiExtend                              
*│　类    名： IHttpAPIInvoker                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.HttpApiExtend
{
    public interface IHttpAPIInvoker
    {
        string InvokeApi(string url, string version = "1");
    }
}
