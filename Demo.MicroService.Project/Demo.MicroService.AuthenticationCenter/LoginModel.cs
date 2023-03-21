/**
*┌──────────────────────────────────────────────────────────────┐
*│　描    述：                                                    
*│　作    者：赵永杰                                             
*│　版    本：1.0                                                 
*│　创建时间：2023/3/21 13:59:41                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.AuthenticationCenter                              
*│　类    名： LoginModel                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.AuthenticationCenter
{
    public class LoginModel
    {
        public string name { get; set; }
        public string password { get; set; } 
        public string TenantCode { get; set; }

    }
}
