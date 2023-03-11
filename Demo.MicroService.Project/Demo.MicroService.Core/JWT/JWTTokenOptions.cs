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
*│　创建时间：2023/3/11 15:19:16                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.Core.JWT                              
*│　类    名： JWTTokenOptions                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.Core.JWT
{
    public class JWTTokenOptions
    {
        public const string JWTTokenOption = "JWTTokenOptions";
        public string Audience
        {
            get;
            set;
        }
        public string SecurityKey
        {
            get;
            set;
        }
        //public SigningCredentials Credentials
        //{
        //    get;
        //    set;
        //}
        public string Issuer
        {
            get;
            set;
        }
    }
}
