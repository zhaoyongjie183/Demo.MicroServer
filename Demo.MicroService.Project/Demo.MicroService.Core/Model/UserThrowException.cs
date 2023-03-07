/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述： 自定义异常                                                   
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/7 16:30:20                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.Model                              
*│　类    名： UserThrowException                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.Model
{
    internal class UserThrowException : Exception
    {
        public UserThrowException(string message) : base(message)
        {

        }
    }
}
