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
*│　创建时间：2023/3/14 9:27:06                            
*└──────────────────────────────────────────────────────────────┘
*┌──────────────────────────────────────────────────────────────┐
*│　命名空间： Demo.MicroService.BusinessModel.DTO.Tenant                              
*│　类    名： TSysUserDTO                                      
*└──────────────────────────────────────────────────────────────┘
*/
namespace Demo.MicroService.BusinessModel.DTO.Tenant
{
    public class TSysUserDTO
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 登录密码
        /// </summary>
        public string UserPassword { get; set; } = string.Empty;

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Mail { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; } = string.Empty;

        /// <summary>
        /// 联系手机号
        /// </summary>
        public string Mobile { get; set; } = string.Empty;


        /// <summary>
        /// 备注
        /// </summary>
        public string Memo { get; set; } = string.Empty;

        public string TenantCode { get; set; }

    }
}
