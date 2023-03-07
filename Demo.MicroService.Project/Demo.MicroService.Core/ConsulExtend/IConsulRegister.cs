/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/7 15:37:48                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.ConsulExtend                              
*│　类    名： IConsulRegister                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.ConsulExtend
{
    public interface IConsulRegister
    {
        /// <summary>
        /// 注册consul
        /// </summary>
        /// <returns></returns>
        Task UseConsulRegist();
    }
}
